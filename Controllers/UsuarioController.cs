using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using proyectoWeb_GYM.Models;
using System.Reflection.Metadata;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using Microsoft.CodeAnalysis;
using System.Xml.Linq;
using Microsoft.Extensions.Hosting.Internal;

namespace proyectoWeb_GYM.Controllers
{
    public class UsuarioController : Controller
    {

        private readonly gymDbContext _gymContext;
        private readonly IWebHostEnvironment _hostEnvironment;

        public UsuarioController(gymDbContext gymContext, IWebHostEnvironment hostEnvironment)
        {
            _gymContext = gymContext;
            _hostEnvironment = hostEnvironment;
        }




        public IActionResult Index()
        {
            ViewData["listaMembresia"] = new SelectList(ListarMembresia(), "id_membresia", "nombre_membresia");
            ViewData["listadoUsuarios"] = listarUsuario();
			ViewData["listadoPagos"] = listarPago();
			return View();
        }

        public IActionResult InsertarUsuario(Usuario nuevoUsuario)
        {
            _gymContext.Add(nuevoUsuario);
            _gymContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public List<Membresia> ListarMembresia()
        {

            var listaMembresia = (from Membresia in _gymContext.Membresia select Membresia).ToList();

            return listaMembresia;

        }

      

        public List<UsuariosReportados> listarUsuario()
        {
            List<UsuariosReportados> listadoUsuarios = (from u in _gymContext.Usuario
                                                  join m in _gymContext.Membresia on u.id_membresia equals m.id_membresia
                                                  join i in _gymContext.Info_usuario on u.id_usuario equals i.id_usuario
                                                  

                                                  select new UsuariosReportados
                                                  {
                                                      id = u.id_usuario,
                                                      nombre = u.nombre,
                                                      apellido = u.apellido,
                                                      nombre_mem = m.nombre_membresia,
                                                      correo = u.correo,
                                                      edad = i.edad,
                                                      fecha = u.fecha_creacion_cuenta
                                                  }).ToList();


            return listadoUsuarios;
        }

        public void GenerarPDF(List<UsuariosReportados> listadoUsuarios, string rutaArchivo)
        {
            iTextSharp.text.Document doc = new iTextSharp.text.Document(PageSize.A4.Rotate());
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(rutaArchivo, FileMode.Create));
            doc.Open();


            string virtualPath ="wwwroot/images/icon-gymbro.png";           
            string rutaFoto = Path.Combine(_hostEnvironment.ContentRootPath, virtualPath);
            Image foto = Image.GetInstance(rutaFoto);
            foto.ScaleToFit(200f, 200f); 
            foto.Alignment = Element.ALIGN_CENTER;
            doc.Add(foto);


			float[] widths = new float[] { 1f, 1f };
			PdfPTable table1 = new PdfPTable(2);
            table1.TotalWidth = doc.PageSize.Width - 80;
			table1.LockedWidth = true;
			table1.SpacingBefore = 30f;
			table1.SpacingAfter = 30f;

			
			PdfPCell cell1 = new PdfPCell(new Phrase("Lista de usuarios registrados:", new Font(Font.FontFamily.HELVETICA, 14, Font.BOLD)));
			cell1.Border = PdfPCell.NO_BORDER;
			table1.AddCell(cell1);

			
			PdfPCell cell2 = new PdfPCell(new Phrase(DateTime.Now.ToString()));
			cell2.Border = PdfPCell.NO_BORDER;
			cell2.HorizontalAlignment = Element.ALIGN_RIGHT;
			table1.AddCell(cell2);

			
			doc.Add(table1);

			PdfPTable table = new PdfPTable(7); 
            table.WidthPercentage = 100f;
            table.DefaultCell.FixedHeight = 20f;

			// Agregar encabezados de tabla

			table.AddCell("ID");
            table.AddCell("Nombre");
            table.AddCell("Apellido");
            table.AddCell("Membresía");
            table.AddCell("Correo");
            table.AddCell("Edad");
            table.AddCell("Creación Cuenta");

        
            foreach (UsuariosReportados usuario in listadoUsuarios)
            {
                table.AddCell(usuario.id.ToString());
                table.AddCell(usuario.nombre);
                table.AddCell(usuario.apellido);
                table.AddCell(usuario.nombre_mem);
                table.AddCell(usuario.correo);
                table.AddCell(usuario.edad.ToString());
                table.AddCell(usuario.fecha.ToString());
            }

            
            doc.Add(table);



            // Cerrar el documento
            doc.Close();
        }

        [HttpGet("GenerarPDF")]
        public ActionResult GenerarPDF()
        {

            List<UsuariosReportados> listado = listarUsuario();
            string nombreArchivo = "usuarios_registrados.pdf";
            string rutaCarpetaArchivos = Path.Combine(_hostEnvironment.ContentRootPath, "ArchivosPDF");

            // Verificar si la carpeta no existe y crearla si es necesario
            if (!Directory.Exists(rutaCarpetaArchivos))
            {
                Directory.CreateDirectory(rutaCarpetaArchivos);
            }

            string rutaArchivoPDF = Path.Combine(rutaCarpetaArchivos, nombreArchivo);

            GenerarPDF(listado, rutaArchivoPDF);

            ViewBag.NombreArchivo = nombreArchivo;
            return View("VistaPDF");
        }

        [HttpGet("DescargarPDF")]
        public ActionResult DescargarPDF()
        {
            string nombreArchivo = "usuarios_registrados.pdf";
            string rutaCarpetaArchivos = Path.Combine(_hostEnvironment.ContentRootPath, "ArchivosPDF");
            string rutaArchivoPDF = Path.Combine(rutaCarpetaArchivos, nombreArchivo);

            byte[] archivoBytes = System.IO.File.ReadAllBytes(rutaArchivoPDF);
            return File(archivoBytes, "application/pdf", nombreArchivo);
        }




        //seccion para pagos

        public List<PagosReportados> listarPago()
        {
            List<PagosReportados> listadoPagos = (from r in _gymContext.Registro_pago
                                                  join u in _gymContext.Usuario on r.id_usuario equals u.id_usuario
                                                  join m in _gymContext.Membresia on u.id_membresia equals m.id_membresia


                                                  select new PagosReportados
                                                  {
                                                      id_pago = r.id_pago,
                                                      nombre = u.nombre,
                                                      apellido = u.apellido,
                                                      correo = u.correo,
                                                      telefono = u.telefono,
                                                      nombre_mem = m.nombre_membresia,
                                                      fecha = u.fecha_creacion_cuenta
                                                  }).ToList();


            return listadoPagos;
        }



        public void GenerarPDF2(List<PagosReportados> listadoPagos, string rutaArchivo)
        {
            iTextSharp.text.Document doc = new iTextSharp.text.Document(PageSize.A4.Rotate());
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(rutaArchivo, FileMode.Create));
            doc.Open();

            string Foto = Url.Content("C:\\SCRUM\\proyectoWeb-GYM\\wwwroot\\icon-gymbro.png");

            string rutaFoto = Path.Combine(_hostEnvironment.ContentRootPath, Foto);
            Image foto = Image.GetInstance(rutaFoto);
            foto.ScaleToFit(200f, 200f);
            foto.Alignment = Element.ALIGN_CENTER;
            doc.Add(foto);


            float[] widths = new float[] { 1f, 1f };
            PdfPTable table1 = new PdfPTable(2);
            table1.TotalWidth = doc.PageSize.Width - 80;
            table1.LockedWidth = true;
            table1.SpacingBefore = 30f;
            table1.SpacingAfter = 30f;


            PdfPCell cell1 = new PdfPCell(new Phrase("Lista de pagos registrados:", new Font(Font.FontFamily.HELVETICA, 14, Font.BOLD)));
            cell1.Border = PdfPCell.NO_BORDER;
            table1.AddCell(cell1);


            PdfPCell cell2 = new PdfPCell(new Phrase(DateTime.Now.ToString()));
            cell2.Border = PdfPCell.NO_BORDER;
            cell2.HorizontalAlignment = Element.ALIGN_RIGHT;
            table1.AddCell(cell2);


            doc.Add(table1);

            PdfPTable table = new PdfPTable(7);
            table.WidthPercentage = 100f;
            table.DefaultCell.FixedHeight = 20f;

            // Agregar encabezados de tabla

            table.AddCell("ID Pago");
            table.AddCell("Nombre");
            table.AddCell("Apellido");
            table.AddCell("Correo");
            table.AddCell("Telèfono");
            table.AddCell("Membresía");
            table.AddCell("Fecha del Pago");


            foreach (PagosReportados pagos in listadoPagos)
            {
                table.AddCell(pagos.id_pago);
                table.AddCell(pagos.nombre);
                table.AddCell(pagos.apellido);
                table.AddCell(pagos.correo);
                table.AddCell(pagos.telefono);
                table.AddCell(pagos.nombre_mem);
                table.AddCell(pagos.fecha.ToString());
            }


            doc.Add(table);



            // Cerrar el documento
            doc.Close();
        }



        [HttpGet("GenerarPDF2")]
        public ActionResult GenerarPDF2()
        {

            List<PagosReportados> listado = listarPago();
            string nombreArchivo = "pagos_registrados.pdf";
            string rutaCarpetaArchivos = Path.Combine(_hostEnvironment.ContentRootPath, "ArchivosPDF");

            // Verificar si la carpeta no existe y crearla si es necesario
            if (!Directory.Exists(rutaCarpetaArchivos))
            {
                Directory.CreateDirectory(rutaCarpetaArchivos);
            }

            string rutaArchivoPDF = Path.Combine(rutaCarpetaArchivos, nombreArchivo);

            GenerarPDF2(listado, rutaArchivoPDF);

            ViewBag.NombreArchivo2 = nombreArchivo;
            return View("VistaPDF2");
        }


        [HttpGet("DescargarPDF2")]
        public ActionResult DescargarPDF2()
        {
            string nombreArchivo = "pagos_registrados.pdf";
            string rutaCarpetaArchivos = Path.Combine(_hostEnvironment.ContentRootPath, "ArchivosPDF");
            string rutaArchivoPDF = Path.Combine(rutaCarpetaArchivos, nombreArchivo);

            byte[] archivoBytes = System.IO.File.ReadAllBytes(rutaArchivoPDF);
            return File(archivoBytes, "application/pdf", nombreArchivo);
        }




    }

    public class UsuariosReportados
    {
        public int? id { get; set; }
        public string? nombre { get; set; }
        public string? apellido { get; set; }
        public string? nombre_mem { get; set; }
        public string? correo { get; set; }
        public int? edad { get; set; }
        public DateTime? fecha { get; set; }
    }


    public class PagosReportados
    {
        public string? id_pago { get; set; }
        public string? nombre { get; set; }
        public string? apellido { get; set; }
        public string? correo { get; set; }

        public string? telefono { get; set; }
        public string? nombre_mem { get; set; }
        public DateTime? fecha { get; set; }
    }




}


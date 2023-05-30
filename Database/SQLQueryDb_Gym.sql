USE [db_Gym]
GO
/****** Object:  Table [dbo].[Info_usuario]    Script Date: 30/5/2023 11:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Info_usuario](
	[id_info_usuario] [int] IDENTITY(1,1) NOT NULL,
	[edad] [int] NULL,
	[peso] [float] NULL,
	[estatura] [float] NULL,
	[IMC] [float] NULL,
	[foto] [varbinary](max) NULL,
	[id_usuario] [int] NULL,
	[correo] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_info_usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Membresia]    Script Date: 30/5/2023 11:38:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Membresia](
	[id_membresia] [int] IDENTITY(1,1) NOT NULL,
	[nombre_membresia] [varchar](20) NOT NULL,
	[precio] [float] NULL,
	[vendidas] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_membresia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pago]    Script Date: 30/5/2023 11:38:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pago](
	[id_pago] [varchar](15) NOT NULL,
	[fecha_pago] [datetime] NULL,
	[fecha_renovacion] [datetime] NULL,
	[estado] [int] NOT NULL,
	[id_membresia] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_pago] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Registro_pago]    Script Date: 30/5/2023 11:38:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Registro_pago](
	[id_registro_pago] [int] IDENTITY(1,1) NOT NULL,
	[id_usuario] [int] NULL,
	[id_pago] [varchar](15) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_registro_pago] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 30/5/2023 11:38:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[id_usuario] [int] IDENTITY(1,1) NOT NULL,
	[correo] [varchar](100) NOT NULL,
	[clave] [varchar](20) NOT NULL,
	[nombre] [varchar](50) NULL,
	[apellido] [varchar](50) NULL,
	[direccion] [varchar](500) NULL,
	[telefono] [varchar](9) NULL,
	[num_cuenta] [varchar](20) NULL,
	[nombre_titular] [varchar](500) NULL,
	[cvv] [varchar](3) NULL,
	[mes] [int] NULL,
	[anio] [int] NULL,
	[id_membresia] [int] NULL,
	[estado] [bit] NULL,
	[fecha_creacion_cuenta] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Info_usuario] ON 
GO
INSERT [dbo].[Info_usuario] ([id_info_usuario], [edad], [peso], [estatura], [IMC], [foto], [id_usuario], [correo]) VALUES (7, 18, 180, 190, 0.94736842105263153, NULL, 4, N'123@gmail.com')
GO
SET IDENTITY_INSERT [dbo].[Info_usuario] OFF
GO
SET IDENTITY_INSERT [dbo].[Membresia] ON 
GO
INSERT [dbo].[Membresia] ([id_membresia], [nombre_membresia], [precio], [vendidas]) VALUES (1, N'Mensual', 3, 2)
GO
SET IDENTITY_INSERT [dbo].[Membresia] OFF
GO
INSERT [dbo].[Pago] ([id_pago], [fecha_pago], [fecha_renovacion], [estado], [id_membresia]) VALUES (N'4E1A96E4-6E42-4', CAST(N'2023-04-18T19:04:50.900' AS DateTime), CAST(N'2023-05-18T19:04:50.900' AS DateTime), 0, 1)
GO
INSERT [dbo].[Pago] ([id_pago], [fecha_pago], [fecha_renovacion], [estado], [id_membresia]) VALUES (N'9D765188-CF52-4', CAST(N'2023-04-18T20:12:57.090' AS DateTime), CAST(N'2023-05-18T20:12:57.090' AS DateTime), 0, 1)
GO
SET IDENTITY_INSERT [dbo].[Registro_pago] ON 
GO
INSERT [dbo].[Registro_pago] ([id_registro_pago], [id_usuario], [id_pago]) VALUES (2, 3, N'4E1A96E4-6E42-4')
GO
INSERT [dbo].[Registro_pago] ([id_registro_pago], [id_usuario], [id_pago]) VALUES (3, 4, N'9D765188-CF52-4')
GO
SET IDENTITY_INSERT [dbo].[Registro_pago] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuario] ON 
GO
INSERT [dbo].[Usuario] ([id_usuario], [correo], [clave], [nombre], [apellido], [direccion], [telefono], [num_cuenta], [nombre_titular], [cvv], [mes], [anio], [id_membresia], [estado], [fecha_creacion_cuenta]) VALUES (3, N'maingragas55@gmail.com', N'1234aaxd', N'William', N'Cuellar', N'Test', N'67293812', N'12345678910', N'William Cuellar', N'414', 6, 2023, 1, 1, CAST(N'2023-04-18T19:04:50.897' AS DateTime))
GO
INSERT [dbo].[Usuario] ([id_usuario], [correo], [clave], [nombre], [apellido], [direccion], [telefono], [num_cuenta], [nombre_titular], [cvv], [mes], [anio], [id_membresia], [estado], [fecha_creacion_cuenta]) VALUES (4, N'123@gmail.com', N'1234aaxd', N'Juan', N'Perez', N'UNICAES', N'12345678', N'123456789012345', N'JUAN PEREZ', N'234', 8, 2023, 1, 1, CAST(N'2023-04-18T20:12:57.087' AS DateTime))
GO
INSERT [dbo].[Usuario] ([id_usuario], [correo], [clave], [nombre], [apellido], [direccion], [telefono], [num_cuenta], [nombre_titular], [cvv], [mes], [anio], [id_membresia], [estado], [fecha_creacion_cuenta]) VALUES (5, N'admin@gmail.com', N'admin', N'Administrador', N'admin', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, CAST(N'2023-05-08T21:36:25.840' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Usuario] OFF
GO
ALTER TABLE [dbo].[Pago] ADD  DEFAULT (getdate()) FOR [fecha_pago]
GO
ALTER TABLE [dbo].[Usuario] ADD  DEFAULT ((1)) FOR [estado]
GO
ALTER TABLE [dbo].[Usuario] ADD  DEFAULT (getdate()) FOR [fecha_creacion_cuenta]
GO
ALTER TABLE [dbo].[Info_usuario]  WITH CHECK ADD FOREIGN KEY([id_usuario])
REFERENCES [dbo].[Usuario] ([id_usuario])
GO
ALTER TABLE [dbo].[Info_usuario]  WITH CHECK ADD FOREIGN KEY([id_usuario])
REFERENCES [dbo].[Usuario] ([id_usuario])
GO
ALTER TABLE [dbo].[Pago]  WITH CHECK ADD FOREIGN KEY([id_membresia])
REFERENCES [dbo].[Membresia] ([id_membresia])
GO
ALTER TABLE [dbo].[Pago]  WITH CHECK ADD FOREIGN KEY([id_membresia])
REFERENCES [dbo].[Membresia] ([id_membresia])
GO
ALTER TABLE [dbo].[Registro_pago]  WITH CHECK ADD FOREIGN KEY([id_pago])
REFERENCES [dbo].[Pago] ([id_pago])
GO
ALTER TABLE [dbo].[Registro_pago]  WITH CHECK ADD FOREIGN KEY([id_pago])
REFERENCES [dbo].[Pago] ([id_pago])
GO
ALTER TABLE [dbo].[Registro_pago]  WITH CHECK ADD FOREIGN KEY([id_usuario])
REFERENCES [dbo].[Usuario] ([id_usuario])
GO
ALTER TABLE [dbo].[Registro_pago]  WITH CHECK ADD FOREIGN KEY([id_usuario])
REFERENCES [dbo].[Usuario] ([id_usuario])
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD FOREIGN KEY([id_membresia])
REFERENCES [dbo].[Membresia] ([id_membresia])
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD FOREIGN KEY([id_membresia])
REFERENCES [dbo].[Membresia] ([id_membresia])
GO
/****** Object:  StoredProcedure [dbo].[sp_AgregarInformacionPersonal]    Script Date: 30/5/2023 11:38:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_AgregarInformacionPersonal](@edad INT,
@peso FLOAT, @estatura FLOAT, @correo VARCHAR(100))
AS
BEGIN
	DECLARE @imc FLOAT
	SET @imc = @peso / @estatura;

	if(exists(SELECT id_usuario FROM info_usuario WHERE correo = @correo))
	  
	  SELECT '0'

	ELSE

	INSERT INTO info_usuario(edad, peso, estatura, IMC, foto, id_usuario, correo) VALUES(@edad, @peso, @estatura, @imc, null,  
	(SELECT id_usuario from usuario where correo=@correo), @correo) 

	SELECT id_usuario FROM usuario WHERE correo = @correo;
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_GenerarPago_ingreso]    Script Date: 30/5/2023 11:38:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[sp_GenerarPago_ingreso](@correo VARCHAR(100), @id_membresia INT)
AS
BEGIN
	DECLARE @fecha_renovacion DATETIME
	DECLARE @fecha_pago DATETIME

	SET @fecha_pago=GETDATE()
	SET @fecha_renovacion = DATEADD(day, 30, @fecha_pago)

	DECLARE @guid UNIQUEIDENTIFIER = NEWID()
    DECLARE @pagoid varchar(15)
    SET @pagoid = SUBSTRING(CONVERT(varchar(255), @guid), 1, 15)

	INSERT INTO pago VALUES (@pagoid, @fecha_pago, @fecha_renovacion, 0, @id_membresia)
	INSERT INTO registro_pago VALUES (@pagoid, (SELECT id_usuario from usuario where correo=@correo))
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_ModificarInformacionPersonal]    Script Date: 30/5/2023 11:38:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[sp_ModificarInformacionPersonal](@edad INT,
@peso FLOAT, @estatura FLOAT, @correo VARCHAR(100))
AS
BEGIN
	DECLARE @imc FLOAT
	SET @imc = @peso / @estatura;

	IF(exists(SELECT * FROM info_usuario WHERE id_usuario = (SELECT id_usuario from usuario where correo=@correo)))
		UPDATE info_usuario SET edad=@edad, peso=@peso, estatura=@estatura,
		IMC=@imc WHERE id_usuario = (SELECT id_usuario from usuario where correo=@correo)
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_RegistrarUsuario]    Script Date: 30/5/2023 11:38:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[sp_RegistrarUsuario](@nombre varchar(50),@apellido varchar(50),
@direccion varchar(500), @telefono varchar(9), @correo VARCHAR(100), @clave VARCHAR(20), @num_cuenta varchar(20),
@nombre_titular varchar(500), @cvv varchar(3),@mes int, @anio int, @id_membresia int, @registrado BIT OUTPUT,
@mensaje VARCHAR(500) OUTPUT)
AS
BEGIN
	DECLARE @resultado VARCHAR(20)

	IF(exists(SELECT * FROM usuario WHERE correo = @correo AND clave = @clave))
	   SELECT 'El usuario ya existe' AS resultado 
    ELSE BEGIN
	    IF (@correo LIKE '%_@__%.__%') BEGIN
			 IF (PATINDEX('%[^a-zA-Z0-9]%', @clave) = 0) BEGIN
				--DECLARE @fecha_caducidad VARCHAR(6)
				--SET @fecha_caducidad = CONVERT(VARCHAR(2), @mes) + '/' + CONVERT(VARCHAR(4), @anio)
				INSERT INTO Usuario(nombre, apellido, direccion, telefono, 
	                          correo, clave, num_cuenta, nombre_titular, cvv, mes, anio, id_membresia) 
							  VALUES 
					     (@nombre, @apellido,@direccion, @telefono,
						      @correo, @clave, @num_cuenta, @nombre_titular, @cvv, @mes, @anio, @id_membresia);
				DECLARE @fecha_renovacion datetime
				DECLARE @fecha_pago datetime

				SET @fecha_pago=GETDATE()
				SET @fecha_renovacion = DATEADD(day, 30, @fecha_pago)

				DECLARE @guid UNIQUEIDENTIFIER = NEWID()
				DECLARE @pagoid varchar(15)
				SET @pagoid = SUBSTRING(CONVERT(varchar(255), @guid), 1, 15)

				INSERT INTO pago VALUES (@pagoid, @fecha_pago, @fecha_renovacion, 0, @id_membresia)
				INSERT INTO registro_pago VALUES ((SELECT id_usuario from usuario where correo=@correo), @pagoid)
				
				UPDATE membresia SET vendidas = vendidas + 1 WHERE id_membresia = @id_membresia

				SET @registrado = 1
				SET @mensaje = 'Usuario Registrado'
			 END
			 ELSE
				SET @registrado = 0
				SET @mensaje = 'Correo o contraseña inválidos.'
		END 
		ELSE
			SET @registrado = 0
			SET @mensaje = 'Correo o contraseña inválidos.'
	END
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_ValidarUsuario]    Script Date: 30/5/2023 11:38:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_ValidarUsuario](@correo VARCHAR(100), @clave VARCHAR(20))
AS
BEGIN
    IF(exists(SELECT * FROM usuario WHERE correo = @correo AND clave = @clave))
	   SELECT id_usuario, nombre, apellido, correo FROM usuario WHERE correo = @correo AND clave = @clave;

    ELSE
	    SELECT '0'

END;
GO

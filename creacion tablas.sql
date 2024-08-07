USE [TributoDB]
GO
/******* Object:  Table [dbo].[Contribuyentes]    Script Date: 30/07/2024 9:39:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contribuyentes](
	[IdContribuyente] [int] IDENTITY(1,1) NOT NULL,
	[NumeroDocumentoContribuyente] [nvarchar](11) NOT NULL,
	[ApellidoNombreContribuyente] [nvarchar](200) NOT NULL,
	[DomicilioCalleContribuyente] [nvarchar](200) NOT NULL,
	[DomicilioNumeroContribuyente] [nvarchar](max) NOT NULL,
	[TelefonoContribuyente] [nvarchar](15) NOT NULL,
	[SexoContribuyente] [nvarchar](1) NOT NULL,
	[FechaNacimientoContribuyente] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Contribuyentes] PRIMARY KEY CLUSTERED 
(
	[IdContribuyente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PadronBoletas]    Script Date: 30/07/2024 9:39:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PadronBoletas](
	[IdBoleta] [int] IDENTITY(1,1) NOT NULL,
	[NumeroPadron] [int] NOT NULL,
	[Periodo] [int] NOT NULL,
	[Importe] [decimal](12, 2) NOT NULL,
	[Vencimiento] [datetime2](7) NOT NULL,
	[Pagado] [bit] NOT NULL,
	[Importe2] [decimal](12, 2) NOT NULL,
	[Vencimiento2] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_PadronBoletas] PRIMARY KEY CLUSTERED 
(
	[IdBoleta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PadronContribuyentes]    Script Date: 30/07/2024 9:39:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PadronContribuyentes](
	[IdContribuyente] [int] NOT NULL,
	[IdTributoMunicipal] [int] NOT NULL,
	[NumeroPadron] [int] NOT NULL,
	[Estado] [bit] NOT NULL,
 CONSTRAINT [PK_PadronContribuyentes] PRIMARY KEY CLUSTERED 
(
	[IdContribuyente] ASC,
	[IdTributoMunicipal] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_PadronContribuyentes_NumeroPadron] UNIQUE NONCLUSTERED 
(
	[NumeroPadron] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TributosMunicipales]    Script Date: 30/07/2024 9:39:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TributosMunicipales](
	[IdTributo] [int] IDENTITY(1,1) NOT NULL,
	[NombreTributo] [nvarchar](100) NOT NULL,
	[Estado] [bit] NOT NULL,
	[Sintetico] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_TributosMunicipales] PRIMARY KEY CLUSTERED 
(
	[IdTributo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PadronBoletas] ADD  DEFAULT ((0.0)) FOR [Importe2]
GO
ALTER TABLE [dbo].[PadronBoletas] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [Vencimiento2]
GO
ALTER TABLE [dbo].[TributosMunicipales] ADD  DEFAULT (N'') FOR [Sintetico]
GO
ALTER TABLE [dbo].[PadronBoletas]  WITH CHECK ADD  CONSTRAINT [FK_PadronBoletas_PadronContribuyentes_NumeroPadron] FOREIGN KEY([NumeroPadron])
REFERENCES [dbo].[PadronContribuyentes] ([NumeroPadron])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PadronBoletas] CHECK CONSTRAINT [FK_PadronBoletas_PadronContribuyentes_NumeroPadron]
GO
ALTER TABLE [dbo].[PadronContribuyentes]  WITH CHECK ADD  CONSTRAINT [FK_PadronContribuyentes_Contribuyentes_IdContribuyente] FOREIGN KEY([IdContribuyente])
REFERENCES [dbo].[Contribuyentes] ([IdContribuyente])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PadronContribuyentes] CHECK CONSTRAINT [FK_PadronContribuyentes_Contribuyentes_IdContribuyente]
GO
ALTER TABLE [dbo].[PadronContribuyentes]  WITH CHECK ADD  CONSTRAINT [FK_PadronContribuyentes_TributosMunicipales_IdTributoMunicipal] FOREIGN KEY([IdTributoMunicipal])
REFERENCES [dbo].[TributosMunicipales] ([IdTributo])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PadronContribuyentes] CHECK CONSTRAINT [FK_PadronContribuyentes_TributosMunicipales_IdTributoMunicipal]
GO

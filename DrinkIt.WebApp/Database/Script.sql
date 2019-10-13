USE [master]
GO
/****** Object:  Database [drinkit]    Script Date: 13/10/2019 18:59:55 ******/
CREATE DATABASE [drinkit]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'drinkit', FILENAME = N'C:\Users\gabriel\drinkit.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'drinkit_log', FILENAME = N'C:\Users\gabriel\drinkit_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [drinkit] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [drinkit].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [drinkit] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [drinkit] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [drinkit] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [drinkit] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [drinkit] SET ARITHABORT OFF 
GO
ALTER DATABASE [drinkit] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [drinkit] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [drinkit] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [drinkit] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [drinkit] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [drinkit] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [drinkit] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [drinkit] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [drinkit] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [drinkit] SET  DISABLE_BROKER 
GO
ALTER DATABASE [drinkit] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [drinkit] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [drinkit] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [drinkit] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [drinkit] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [drinkit] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [drinkit] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [drinkit] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [drinkit] SET  MULTI_USER 
GO
ALTER DATABASE [drinkit] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [drinkit] SET DB_CHAINING OFF 
GO
ALTER DATABASE [drinkit] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [drinkit] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [drinkit] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [drinkit] SET QUERY_STORE = OFF
GO
USE [drinkit]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [drinkit]
GO
/****** Object:  Table [dbo].[Bebidas]    Script Date: 13/10/2019 18:59:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bebidas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](50) NULL,
	[Descricao] [varchar](50) NULL,
	[TipoBebida] [int] NULL,
	[Marca] [varchar](50) NULL,
	[Valor] [decimal](18, 2) NULL,
	[Volume] [varchar](50) NULL,
	[Peso] [varchar](50) NULL,
	[Sabor] [varchar](50) NULL,
	[Lote] [varchar](50) NULL,
	[DataFabricacao] [smalldatetime] NULL,
	[DataValidade] [smalldatetime] NULL,
	[Fabricante] [varchar](50) NULL,
	[Embalagem] [varchar](50) NULL,
	[CodigoBarras] [varchar](50) NULL,
	[Alcoolico] [bit] NULL,
	[Teor] [decimal](18, 0) NULL,
	[Gaseificada] [bit] NULL,
	[ContemGluten] [bit] NULL,
	[DicaConservacao] [varchar](50) NULL,
	[Status] [bit] NULL,
	[CaminhoImagem] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cartoes]    Script Date: 13/10/2019 18:59:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cartoes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClienteId] [int] NULL,
	[Numero] [varchar](50) NULL,
	[CodigoSeguranca] [int] NULL,
	[Bandeira] [int] NULL,
	[NomeTitular] [varchar](50) NULL,
	[Preferencial] [bit] NULL,
	[MesValidade] [nchar](10) NULL,
	[AnoValidade] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Clientes]    Script Date: 13/10/2019 18:59:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clientes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](50) NULL,
	[Genero] [varchar](50) NULL,
	[Cpf] [varchar](50) NULL,
	[Telefone] [varchar](50) NULL,
	[DataNascimento] [smalldatetime] NULL,
	[Email] [varchar](50) NULL,
	[Login] [varchar](50) NULL,
	[Senha] [varchar](50) NULL,
	[Status] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cupons]    Script Date: 13/10/2019 18:59:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cupons](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Descricao] [varchar](50) NULL,
	[IdTipo] [int] NULL,
	[DtCriacao] [smalldatetime] NULL,
	[DtExpiracao] [smalldatetime] NULL,
	[Ativo] [bit] NULL,
	[Valor] [decimal](18, 2) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CuponsCliente]    Script Date: 13/10/2019 18:59:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CuponsCliente](
	[IdCliente] [int] NULL,
	[IdCupom] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Enderecos]    Script Date: 13/10/2019 18:59:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Enderecos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClienteId] [int] NULL,
	[Descricao] [varchar](50) NULL,
	[Logradouro] [varchar](50) NULL,
	[Bairro] [varchar](50) NULL,
	[CEP] [varchar](50) NULL,
	[Complemento] [varchar](50) NULL,
	[Cidade] [varchar](50) NULL,
	[Estado] [varchar](50) NULL,
	[Cobranca] [bit] NULL,
	[Entrega] [bit] NULL,
	[Numero] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Estoque]    Script Date: 13/10/2019 18:59:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Estoque](
	[IdBebida] [int] NULL,
	[Qtde] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InativacaoBebidas]    Script Date: 13/10/2019 18:59:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InativacaoBebidas](
	[IdBebida] [int] NULL,
	[DtInativacao] [smalldatetime] NULL,
	[MotivoInativacao] [varchar](150) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ingredientes]    Script Date: 13/10/2019 18:59:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ingredientes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BebidaId] [int] NULL,
	[Descricao] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pedidos]    Script Date: 13/10/2019 18:59:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pedidos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClienteId] [int] NULL,
	[DataCadastro] [smalldatetime] NULL,
	[IdStatus] [int] NULL,
	[DataUltimaAtualizacao] [smalldatetime] NULL,
	[IdEnderecoEntrega] [int] NULL,
	[IdCartao1] [int] NULL,
	[IdCartao2] [int] NULL,
	[ValorTotal] [decimal](18, 2) NULL,
	[IdCupom] [int] NULL,
	[Desconto] [decimal](18, 2) NULL,
	[Frete] [decimal](18, 2) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PedidosItens]    Script Date: 13/10/2019 18:59:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PedidosItens](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PedidoId] [int] NULL,
	[BebidaId] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PedidosStatus]    Script Date: 13/10/2019 18:59:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PedidosStatus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Descricao] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SolicitacoesTroca]    Script Date: 13/10/2019 18:59:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SolicitacoesTroca](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Descricao] [varchar](200) NULL,
	[Status] [int] NULL,
	[IdCliente] [int] NULL,
	[IdPedido] [int] NULL,
	[Data] [smalldatetime] NULL,
	[MotivoReprovacao] [varchar](200) NULL,
	[IdCupom] [int] NULL,
 CONSTRAINT [PK__Solicita__3214EC075960EB9F] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Telefones]    Script Date: 13/10/2019 18:59:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Telefones](
	[IdUsuario] [int] NULL,
	[DDD] [int] NULL,
	[Numero] [varchar](9) NULL,
	[IdTipoTelefone] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoBebida]    Script Date: 13/10/2019 18:59:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoBebida](
	[Id] [int] NULL,
	[Descricao] [varchar](30) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoCupom]    Script Date: 13/10/2019 18:59:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoCupom](
	[Id] [int] NULL,
	[Descricao] [varchar](30) NULL
) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [drinkit] SET  READ_WRITE 
GO

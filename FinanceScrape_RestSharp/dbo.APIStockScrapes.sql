CREATE TABLE [dbo].[APIStockScrapes]
(
	[Id] INT    IDENTITY (1, 1) NOT NULL,
	[StockSymbol] NVARCHAR(MAX) NULL, 
    [StockName] NVARCHAR(MAX) NULL, 
    [LastPrice] NVARCHAR(MAX) NULL, 
    [PriceChange] NVARCHAR(MAX) NULL, 
    [PercentChange] NVARCHAR(MAX) NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC)
)

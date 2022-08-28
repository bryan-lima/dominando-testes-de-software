USE [TestesNerdStoreDb]

INSERT INTO [Vouchers] (Id, Codigo, PercentualDesconto, ValorDesconto, Quantidade, TipoDescontoVoucher, DataValidade, Ativo, Utilizado)
VALUES (NEWID(), 'PROMO-15-REAIS', NULL, 15, 0, 1, GETDATE() + 1, 1, 0)

INSERT INTO [Vouchers] (Id, Codigo, PercentualDesconto, ValorDesconto, Quantidade, TipoDescontoVoucher, DataValidade, Ativo, Utilizado)
VALUES (NEWID(), 'PROMO-10-OFF', 10, null, 50, 0, GETDATE() + 90, 1, 0)

SELECT * FROM [Vouchers]
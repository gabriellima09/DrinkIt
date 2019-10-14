USE drinkit;

insert into Precificacao (Descricao, MargemLucro) values ('Comum', 15.05);
insert into Precificacao (Descricao, MargemLucro) values ('Alcoólicos', 27.47);

insert into TipoCupom (Descricao) values ('Desconto');
insert into TipoCupom (Descricao) values ('Troca');

insert into Cupons (IdTipo, Descricao, Valor, DtCriacao, DtExpiracao, Ativo) values (1, 'Desconto', 15.00, GETDATE(), dateadd(DD, 1, cast(getdate() as date)), 1);
insert into Cupons (IdTipo, Descricao, Valor, DtCriacao, DtExpiracao, Ativo) values (2, 'Troca', 15.00, GETDATE(), dateadd(DD, 1, cast(getdate() as date)), 1);

insert into PedidosStatus (Descricao) values ('FINALIZADO');

insert into TipoBebida (Descricao, IdPrecificacao) values ('Água', 1);
insert into TipoBebida (Descricao, IdPrecificacao) values ('Suco', 1);
insert into TipoBebida (Descricao, IdPrecificacao) values ('Cerveja', 2);
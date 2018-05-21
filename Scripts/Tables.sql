CREATE Table dbo.tblConceptos(
    conNumero int identity(1,1) primary key,
    conCodigo varchar(20) not null,
    conNombre varchar(500) not null,
    conParammetro varchar(50) not null,
    conTipo varchar(5) not null,
    conInactivo bit not null
)

go


CREATE Table dbo.tblTiposConcept(
    tipCodigo char(5) primary key,
    tipNombre varchar(100),
    tipInactivo bit
)

go







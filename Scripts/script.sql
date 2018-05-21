
Insert into tblTiposConcept (tipCodigo, tipNombre, tipInactivo)
    Values('TI', 'Tipos Identificación', 0)

go

Insert into tblTiposConcept (tipCodigo, tipNombre, tipInactivo)
    Values('TT', 'Tipos de Telefonos', 0)

go

Insert into tblConceptos (conCodigo, conNombre, conParammetro, conTipo, conInactivo) 
Values('01', 'Cedula de Ciudadania', '0', 'TI', 0 )

GO

Insert into tblConceptos (conCodigo, conNombre, conParammetro, conTipo, conInactivo)
    Values('02', 'Tarjeta de Identidad', '0', 'TI', 0 )

go

Insert into tblConceptos (conCodigo, conNombre, conParammetro, conTipo, conInactivo) 
Values('01', 'Telefono Fijo', '0', 'TT', 0 )

GO

Insert into tblConceptos (conCodigo, conNombre, conParammetro, conTipo, conInactivo)
    Values('02', 'Telefono Celular', '0', 'TT', 0 )

go

Select * from tblConceptos

GO

Select * from tblTiposConcept

exec prcCargarConceptos 'TT'

exec prcCrearConcepto
    @conCodigo="03",
    @conNombre="Telefono Oficina",
    @conParammetro="0",
    @conTipo="TT",
    @conInactivo=false
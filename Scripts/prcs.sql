ALTER PROCEDURE prcCargarConceptos
    @TIPO VARCHAR(5) = NULL,
    @ID INT = NULL
AS

IF(@ID IS NOT NULL)
BEGIN
    SELECT  conNumero, 
            conCodigo, 
            conNombre, 
            conParammetro as conParametro, 
            conTipo, 
            conInactivo 
    FROM TBLCONCEPTOS
    WHERE conNumero = @ID
END
ELSE 
BEGIN
    IF(@TIPO IS NULL)
        BEGIN

            SELECT  conNumero, 
                    conCodigo, 
                    conNombre, 
                    conParammetro as conParametro, 
                    conTipo, 
                    conInactivo 
            FROM TBLCONCEPTOS

        END
    ELSE
        BEGIN

            SELECT  conNumero,
                    conCodigo, 
                    conNombre, 
                    conParammetro as conParametro, 
                    conTipo, 
                    conInactivo 
            FROM TBLCONCEPTOS
            WHERE conTipo = @tipo

        END
END

GO

ALTER PROCEDURE prcCrearConcepto
    @conCodigo varchar(20),
    @conNombre varchar(500),
    @conParammetro varchar(50),
    @conTipo varchar(5),
    @conInactivo bit,
    @RESULT int OUTPUT
AS

Insert into TBLCONCEPTOS (conCodigo,conNombre,conParammetro,conTipo,conInactivo)
    Values(@conCodigo,@conNombre,@conParammetro,@conTipo,@conInactivo)

SET @RESULT= SCOPE_IDENTITY()

GO
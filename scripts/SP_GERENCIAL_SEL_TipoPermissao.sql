Create Procedure SP_GERENCIAL_SEL_TipoPermissao
	@Nome	varchar(150),
	@Skip int,
	@PageSize int,
	@SortColumn int,
	@SorDirect varchar(4) = 'DESC'
AS
	SET NOCOUNT ON;

	SELECT Id, Nome
	FROM TipoPermissao
	Where Nome like '%'+@Nome+'%'
	ORDER BY
		CASE WHEN @SortColumn = 1 AND @SorDirect = 'desc' THEN Nome END DESC,    
        CASE WHEN @SortColumn = 1 AND @SorDirect = 'asc' THEN Nome END ASC
	OFFSET @Skip ROWS
	FETCH NEXT @PageSize ROWS ONLY;

	SELECT Count(*) as TotalRegs from TipoPermissao Where Nome like '%'+@Nome+'%';
GO

SP_GERENCIAL_SEL_TipoPermissao '', 0, 15, 1, 'asc'
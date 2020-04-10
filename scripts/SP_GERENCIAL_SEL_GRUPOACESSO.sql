Create Procedure SP_GERENCIAL_SEL_GRUPOACESSO
	@Nome	varchar(150),
	@TipoId uniqueidentifier,
	@Skip int,
	@PageSize int,
	@SortColumn int,
	@SorDirect varchar(4) = 'DESC'
AS
	SET NOCOUNT ON;

	SELECT 
		g.Id, g.Nome
	FROM 
		GrupoAcesso g
	Where 
		Nome like '%'+@Nome+'%'
		and (@TipoId is null or g.Id in (Select GrupoAcessoId from GrupoAcessoPermissao))
	ORDER BY
		CASE WHEN @SortColumn = 1 AND @SorDirect = 'desc' THEN Nome END DESC,
        CASE WHEN @SortColumn = 1 AND @SorDirect = 'asc' THEN Nome END ASC
	OFFSET @Skip ROWS
	FETCH NEXT @PageSize ROWS ONLY;

	SELECT Count(*) as TotalRegs From GrupoAcesso g Where Nome like '%'+@Nome+'%' and (@TipoId is null or g.Id in (Select GrupoAcessoId from GrupoAcessoPermissao));
GO

SP_GERENCIAL_SEL_GRUPOACESSO '', null, 0, 15, 1, 'asc'
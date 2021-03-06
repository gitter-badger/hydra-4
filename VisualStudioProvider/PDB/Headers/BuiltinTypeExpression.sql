USE [SDKInterfaceLibrary]
GO

/*
	Table: tblSDKHeaderBuiltinTypeExpression

	Generated automatically by DiaHeadersSqlTransformer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=09006a76b6b1ba0d, Author: u164225, Date: 10/13/2016 4:38:10 PM
	Maps to class VisualStudioProvider.PDB.Headers.BuiltinTypeExpression

	<References>
		<Reference>HeaderFile</Reference>
		<Reference>Expression</Reference>
	</References>
*/


CREATE TABLE [dbo].[tblSDKHeaderBuiltinTypeExpression](
		[BuiltinTypeExpressionId] [uniqueidentifier] NOT NULL,
		[HeaderFileId] [uniqueidentifier] NOT NULL,
		[ExpressionId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_tblSDKHeaderBuiltinTypeExpression] PRIMARY KEY CLUSTERED
(
	[BuiltinTypeExpressionId] ASC
)

WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] 

GO

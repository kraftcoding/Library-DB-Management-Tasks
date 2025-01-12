CREATE TABLE [dbo].[Loans] (
    [Id]       INT           IDENTITY (1, 1) NOT NULL,
    [UserId]   INT           NOT NULL,
    [BookId]   INT           NOT NULL,
    [LoanDate] DATETIME2 (7) NOT NULL,
    CONSTRAINT [PK_Loans] PRIMARY KEY CLUSTERED ([Id] ASC)
);


CREATE TABLE [dbo].[Books] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [UserId]      INT            NOT NULL,
    [Title]       NVARCHAR (MAX) NULL,
    [Author]      NVARCHAR (MAX) NULL,
    [ISBN]        NVARCHAR (MAX) NULL,
    [PublishYear] INT            NOT NULL,
    CONSTRAINT [PK_Books] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Books_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Books_UserId]
    ON [dbo].[Books]([UserId] ASC);


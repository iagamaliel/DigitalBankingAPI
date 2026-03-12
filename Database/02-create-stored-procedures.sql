USE DigitalBankingDb
GO

-- ============================================
-- Deposit
-- ============================================
CREATE PROCEDURE sp_CreateDeposit
    @AccountId NVARCHAR(50),
    @Amount DECIMAL(18,2)
AS
BEGIN

    IF @Amount <= 0
        THROW 50001, 'Amount must be greater than zero.', 1;

    IF NOT EXISTS (SELECT 1 FROM Accounts WHERE AccountId = @AccountId)
        THROW 50002, 'Account not found.', 1;

    BEGIN TRAN

        UPDATE Accounts
        SET Balance = Balance + @Amount
        WHERE AccountId = @AccountId;

        INSERT INTO Transactions
        (
            AccountId,
            Type,
            Amount,
            Date,
            Description
        )
        VALUES
        (
            @AccountId,
            'DEPOSIT',
            @Amount,
            GETDATE(),
            'Account deposit'
        );

        SELECT AccountId,CustomerName,Balance
        FROM Accounts
        WHERE AccountId = @AccountId;

    COMMIT TRAN

END
GO


-- ============================================
-- Withdrawal
-- ============================================
CREATE PROCEDURE sp_CreateWithdrawal
    @AccountId NVARCHAR(50),
    @Amount DECIMAL(18,2)
AS
BEGIN

    IF @Amount <= 0
        RAISERROR('Amount must be greater than zero',16,1)

    DECLARE @Balance DECIMAL(18,2)

    SELECT @Balance = Balance
    FROM Accounts
    WHERE AccountId = @AccountId

    IF @Balance IS NULL
        RAISERROR('Account not found',16,1)

    IF @Balance < @Amount
        RAISERROR('Insufficient balance',16,1)

    BEGIN TRAN

        UPDATE Accounts
        SET Balance = Balance - @Amount
        WHERE AccountId = @AccountId

        INSERT INTO Transactions
        VALUES
        (
            @AccountId,
            'WITHDRAWAL',
            @Amount,
            GETDATE(),
            'Account withdrawal'
        )

        SELECT AccountId,CustomerName,Balance
        FROM Accounts
        WHERE AccountId = @AccountId

    COMMIT TRAN

END
GO
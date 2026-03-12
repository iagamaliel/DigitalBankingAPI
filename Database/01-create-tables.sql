CREATE DATABASE DigitalBankingDb;
GO

USE DigitalBankingDb;
GO

-- =============================
-- TABLE: Accounts
-- =============================
CREATE TABLE Accounts(
    AccountId NVARCHAR(50) NOT NULL PRIMARY KEY,
    CustomerName NVARCHAR(200) NOT NULL,
    Balance DECIMAL(18,2) NOT NULL DEFAULT 0,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE()
);

ALTER TABLE Accounts
ADD CONSTRAINT CK_Accounts_Balance CHECK (Balance >= 0);

-- =============================
-- TABLE: InterestHistory
-- =============================
CREATE TABLE InterestHistory(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    AccountId NVARCHAR(50) NOT NULL,
    InterestRate DECIMAL(5,2) NOT NULL,
    CalculatedInterest DECIMAL(18,2) NOT NULL,
    CalculationDate DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_InterestHistory_Accounts
        FOREIGN KEY (AccountId) REFERENCES Accounts(AccountId)
);

-- =============================
-- TABLE: Transactions
-- =============================
CREATE TABLE Transactions(
    TransactionId INT IDENTITY(1,1) PRIMARY KEY,
    AccountId NVARCHAR(50) NOT NULL,
    Type NVARCHAR(20) NOT NULL,
    Amount DECIMAL(18,2) NOT NULL,
    Date DATETIME NOT NULL DEFAULT GETDATE(),
    Description NVARCHAR(500),
    CONSTRAINT FK_Transactions_Accounts
        FOREIGN KEY (AccountId) REFERENCES Accounts(AccountId)
);

-- =============================
-- INDEXES
-- =============================
CREATE INDEX IX_Transactions_AccountId
ON Transactions(AccountId);

CREATE INDEX IX_Transactions_Date
ON Transactions(Date DESC);
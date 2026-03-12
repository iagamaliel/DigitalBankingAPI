USE DigitalBankingDb
GO

INSERT INTO Accounts
(
    AccountId,
    CustomerName,
    Balance
)
VALUES
('ACC1001','John Carter',1500),
('ACC1002','Maria Lopez',2500),
('ACC1003','David Smith',1000);

INSERT INTO Transactions
(
    AccountId,
    Type,
    Amount,
    Description
)
VALUES
('ACC1001','DEPOSIT',1500,'Initial deposit'),
('ACC1002','DEPOSIT',2500,'Initial deposit'),
('ACC1003','DEPOSIT',1000,'Initial deposit');
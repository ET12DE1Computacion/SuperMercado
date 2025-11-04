USE 5to_Supermercado;

ALTER TABLE Cajero
ADD COLUMN IdentityUserId VARCHAR(255);

ALTER TABLE Cajero
ADD INDEX IX_Cajeros_IdentityUserId (IdentityUserId);

ALTER TABLE Cajero
ADD CONSTRAINT FK_Cajeros_AspNetUsers_IdentityUserId
FOREIGN KEY (IdentityUserId)
REFERENCES AspNetUsers(Id)
ON DELETE SET NULL;
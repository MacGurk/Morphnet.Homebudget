CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    PRIMARY KEY (`MigrationId`)
);

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230815204818_Initial')
BEGIN
    CREATE TABLE `Users` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `Name` varchar(50) NOT NULL,
        `Email` varchar(70) NOT NULL,
        `PasswordHash` varbinary(128) NOT NULL,
        `PasswordSalt` varbinary(128) NOT NULL,
        `IsContributor` tinyint(1) NOT NULL,
        PRIMARY KEY (`Id`)
    );
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230815204818_Initial')
BEGIN
    CREATE TABLE `Transactions` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `Date` datetime(6) NOT NULL,
        `UserId` int NOT NULL,
        `Description` varchar(200) NOT NULL,
        `Price` decimal(18,2) NOT NULL,
        `IsSettled` tinyint(1) NOT NULL,
        PRIMARY KEY (`Id`),
        CONSTRAINT `FK_Transactions_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`Id`) ON DELETE CASCADE
    );
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230815204818_Initial')
BEGIN
    INSERT INTO `Users` (`Id`, `Email`, `IsContributor`, `Name`, `PasswordHash`, `PasswordSalt`)
    VALUES (1, '', FALSE, 'admin', 0x502F884F155E33904D9A878483DD66EF, 0xEDFC658B58E29B7C3E9A2A28452E5AD9);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230815204818_Initial')
BEGIN
    CREATE INDEX `IX_Transactions_UserId` ON `Transactions` (`UserId`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230815204818_Initial')
BEGIN
    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20230815204818_Initial', '7.0.5');
END;


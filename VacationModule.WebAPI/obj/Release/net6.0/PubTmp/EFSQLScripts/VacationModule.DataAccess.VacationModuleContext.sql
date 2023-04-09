﻿CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20230404180802_userMigrations') THEN
    CREATE TABLE "Users" (
        "Id" uuid NOT NULL,
        "FirstName" text NOT NULL,
        "LastName" text NOT NULL,
        "Email" text NOT NULL,
        "PasswordHash" bytea NOT NULL,
        "PasswordSalt" bytea NOT NULL,
        "Role" text NOT NULL,
        CONSTRAINT "PK_Users" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20230404180802_userMigrations') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20230404180802_userMigrations', '7.0.4');
    END IF;
END $EF$;
COMMIT;

START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20230405141421_vacationReqMigration') THEN
    CREATE TABLE "VacationRequests" (
        "Id" integer GENERATED BY DEFAULT AS IDENTITY,
        "UserId" uuid NOT NULL,
        "requestedDays" timestamp with time zone[] NULL,
        CONSTRAINT "PK_VacationRequests" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_VacationRequests_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "Users" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20230405141421_vacationReqMigration') THEN
    CREATE INDEX "IX_VacationRequests_UserId" ON "VacationRequests" ("UserId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20230405141421_vacationReqMigration') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20230405141421_vacationReqMigration', '7.0.4');
    END IF;
END $EF$;
COMMIT;

START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20230409001818_firstMigration') THEN
    ALTER TABLE "VacationRequests" ALTER COLUMN "requestedDays" TYPE timestamp without time zone[];
    ALTER TABLE "VacationRequests" ALTER COLUMN "requestedDays" SET NOT NULL;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20230409001818_firstMigration') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20230409001818_firstMigration', '7.0.4');
    END IF;
END $EF$;
COMMIT;

START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20230409003050_azurefirstMigration') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20230409003050_azurefirstMigration', '7.0.4');
    END IF;
END $EF$;
COMMIT;

START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20230409005221_azure1FirstMigration') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20230409005221_azure1FirstMigration', '7.0.4');
    END IF;
END $EF$;
COMMIT;


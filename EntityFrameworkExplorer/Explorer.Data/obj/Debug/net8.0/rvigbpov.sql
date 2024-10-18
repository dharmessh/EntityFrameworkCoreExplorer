CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);

BEGIN TRANSACTION;

CREATE TABLE "Coaches" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Coaches" PRIMARY KEY AUTOINCREMENT,
    "Name" TEXT NOT NULL,
    "CreationDate" TEXT NOT NULL
);

CREATE TABLE "Teams" (
    "TeamId" INTEGER NOT NULL CONSTRAINT "PK_Teams" PRIMARY KEY AUTOINCREMENT,
    "Name" TEXT NULL,
    "CreationDate" TEXT NOT NULL
);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20241009102035_FirstMigration', '8.0.10');

COMMIT;

BEGIN TRANSACTION;

INSERT INTO "Teams" ("TeamId", "CreationDate", "Name")
VALUES (1, '2024-10-09 13:36:27.7707688', 'India');
SELECT changes();

INSERT INTO "Teams" ("TeamId", "CreationDate", "Name")
VALUES (2, '2024-10-09 13:36:27.7707719', 'Bangladesh');
SELECT changes();

INSERT INTO "Teams" ("TeamId", "CreationDate", "Name")
VALUES (3, '2024-10-09 13:36:27.770772', 'Russia');
SELECT changes();


INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20241009133628_SeedDatabase', '8.0.10');

COMMIT;

BEGIN TRANSACTION;

DELETE FROM "Teams"
WHERE "TeamId" = 1;
SELECT changes();


DELETE FROM "Teams"
WHERE "TeamId" = 2;
SELECT changes();


DELETE FROM "Teams"
WHERE "TeamId" = 3;
SELECT changes();


ALTER TABLE "Teams" ADD "Id" INTEGER NOT NULL DEFAULT 0;

ALTER TABLE "Teams" ADD "CoachId" INTEGER NOT NULL DEFAULT 0;

ALTER TABLE "Teams" ADD "CreatedBy" TEXT NULL;

ALTER TABLE "Teams" ADD "LeagueId" INTEGER NOT NULL DEFAULT 0;

ALTER TABLE "Teams" ADD "ModificationDate" TEXT NOT NULL DEFAULT '0001-01-01 00:00:00';

ALTER TABLE "Teams" ADD "ModifiedBy" TEXT NULL;

ALTER TABLE "Coaches" ADD "CreatedBy" TEXT NULL;

ALTER TABLE "Coaches" ADD "ModificationDate" TEXT NOT NULL DEFAULT '0001-01-01 00:00:00';

ALTER TABLE "Coaches" ADD "ModifiedBy" TEXT NULL;

CREATE TABLE "Leagues" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Leagues" PRIMARY KEY AUTOINCREMENT,
    "Name" TEXT NOT NULL,
    "CreationDate" TEXT NOT NULL,
    "ModificationDate" TEXT NOT NULL,
    "CreatedBy" TEXT NULL,
    "ModifiedBy" TEXT NULL
);

CREATE TABLE "Matches" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Matches" PRIMARY KEY AUTOINCREMENT,
    "HomeTeamId" INTEGER NOT NULL,
    "AwayTeamId" INTEGER NOT NULL,
    "TicketPrice" TEXT NOT NULL,
    "Date" TEXT NOT NULL,
    "CreationDate" TEXT NOT NULL,
    "ModificationDate" TEXT NOT NULL,
    "CreatedBy" TEXT NULL,
    "ModifiedBy" TEXT NULL
);

INSERT INTO "Teams" ("Id", "CoachId", "CreatedBy", "CreationDate", "LeagueId", "ModificationDate", "ModifiedBy", "Name", "TeamId")
VALUES (1, 0, NULL, '2024-10-15 10:26:49.869222', 0, '0001-01-01 00:00:00', NULL, 'India', NULL);
SELECT changes();

INSERT INTO "Teams" ("Id", "CoachId", "CreatedBy", "CreationDate", "LeagueId", "ModificationDate", "ModifiedBy", "Name", "TeamId")
VALUES (2, 0, NULL, '2024-10-15 10:26:49.8692244', 0, '0001-01-01 00:00:00', NULL, 'Bangladesh', NULL);
SELECT changes();

INSERT INTO "Teams" ("Id", "CoachId", "CreatedBy", "CreationDate", "LeagueId", "ModificationDate", "ModifiedBy", "Name", "TeamId")
VALUES (3, 0, NULL, '2024-10-15 10:26:49.8692245', 0, '0001-01-01 00:00:00', NULL, 'Russia', NULL);
SELECT changes();


CREATE TABLE "ef_temp_Teams" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Teams" PRIMARY KEY AUTOINCREMENT,
    "CoachId" INTEGER NOT NULL,
    "CreatedBy" TEXT NULL,
    "CreationDate" TEXT NOT NULL,
    "LeagueId" INTEGER NOT NULL,
    "ModificationDate" TEXT NOT NULL,
    "ModifiedBy" TEXT NULL,
    "Name" TEXT NULL,
    "TeamId" INTEGER NULL
);

INSERT INTO "ef_temp_Teams" ("Id", "CoachId", "CreatedBy", "CreationDate", "LeagueId", "ModificationDate", "ModifiedBy", "Name", "TeamId")
SELECT "Id", "CoachId", "CreatedBy", "CreationDate", "LeagueId", "ModificationDate", "ModifiedBy", "Name", "TeamId"
FROM "Teams";

COMMIT;

PRAGMA foreign_keys = 0;

BEGIN TRANSACTION;

DROP TABLE "Teams";

ALTER TABLE "ef_temp_Teams" RENAME TO "Teams";

COMMIT;

PRAGMA foreign_keys = 1;

BEGIN TRANSACTION;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20241015102650_AddedMoreEntities', '8.0.10');

COMMIT;

BEGIN TRANSACTION;

UPDATE "Teams" SET "CreationDate" = '2024-10-15 10:31:31.8483963'
WHERE "Id" = 1;
SELECT changes();


UPDATE "Teams" SET "CreationDate" = '2024-10-15 10:31:31.8483984'
WHERE "Id" = 2;
SELECT changes();


UPDATE "Teams" SET "CreationDate" = '2024-10-15 10:31:31.8483985'
WHERE "Id" = 3;
SELECT changes();


CREATE TABLE "ef_temp_Teams" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Teams" PRIMARY KEY AUTOINCREMENT,
    "CoachId" INTEGER NOT NULL,
    "CreatedBy" TEXT NULL,
    "CreationDate" TEXT NOT NULL,
    "LeagueId" INTEGER NOT NULL,
    "ModificationDate" TEXT NOT NULL,
    "ModifiedBy" TEXT NULL,
    "Name" TEXT NULL
);

INSERT INTO "ef_temp_Teams" ("Id", "CoachId", "CreatedBy", "CreationDate", "LeagueId", "ModificationDate", "ModifiedBy", "Name")
SELECT "Id", "CoachId", "CreatedBy", "CreationDate", "LeagueId", "ModificationDate", "ModifiedBy", "Name"
FROM "Teams";

COMMIT;

PRAGMA foreign_keys = 0;

BEGIN TRANSACTION;

DROP TABLE "Teams";

ALTER TABLE "ef_temp_Teams" RENAME TO "Teams";

COMMIT;

PRAGMA foreign_keys = 1;

BEGIN TRANSACTION;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20241015103132_AddedENtititesAgain', '8.0.10');

COMMIT;

BEGIN TRANSACTION;

INSERT INTO "Leagues" ("Id", "CreatedBy", "CreationDate", "ModificationDate", "ModifiedBy", "Name")
VALUES (1, NULL, '0001-01-01 00:00:00', '0001-01-01 00:00:00', NULL, 'India US League');
SELECT changes();

INSERT INTO "Leagues" ("Id", "CreatedBy", "CreationDate", "ModificationDate", "ModifiedBy", "Name")
VALUES (2, NULL, '0001-01-01 00:00:00', '0001-01-01 00:00:00', NULL, 'Bangladesh UAE League');
SELECT changes();

INSERT INTO "Leagues" ("Id", "CreatedBy", "CreationDate", "ModificationDate", "ModifiedBy", "Name")
VALUES (3, NULL, '0001-01-01 00:00:00', '0001-01-01 00:00:00', NULL, 'Russia Greenland League');
SELECT changes();


UPDATE "Teams" SET "CreationDate" = '2024-10-15 10:48:48.9923643'
WHERE "Id" = 1;
SELECT changes();


UPDATE "Teams" SET "CreationDate" = '2024-10-15 10:48:48.9923666'
WHERE "Id" = 2;
SELECT changes();


UPDATE "Teams" SET "CreationDate" = '2024-10-15 10:48:48.9923667'
WHERE "Id" = 3;
SELECT changes();


INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20241015104849_SeededLeagues', '8.0.10');

COMMIT;


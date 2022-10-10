-- -------------------------------------------------------------
-- TablePlus 5.0.0(454)
--
-- https://tableplus.com/
--
-- Database: webdb
-- Generation Time: 2022-10-10 23:07:23.7840
-- -------------------------------------------------------------


DROP TABLE IF EXISTS "public"."tbl_userrole";
-- This script only contains the table creation statements and does not fully represent the table in the database. It's still missing: indices, triggers. Do not use it as a backup.

-- Table Definition
CREATE TABLE "public"."tbl_userrole" (
    "roleid" text NOT NULL,
    "rolename" text,
    "rolepriority" int4,
    "modifiedutcdate" timestamp DEFAULT now(),
    PRIMARY KEY ("roleid")
);

INSERT INTO "public"."tbl_userrole" ("roleid", "rolename", "rolepriority", "modifiedutcdate") VALUES
('AssociateUser', 'AssociateUser', 1, '2022-02-22 09:42:28.875321'),
('GeneralUser', 'GeneralUser', 2, '2022-02-22 09:42:28.875321'),
('SuperUser', 'SuperUser', 3, '2022-02-22 09:42:28.875321'),
('SystemUser', 'SystemUser', 4, '2022-02-22 09:42:28.875321');

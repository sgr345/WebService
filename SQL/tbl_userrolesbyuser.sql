-- -------------------------------------------------------------
-- TablePlus 5.0.0(454)
--
-- https://tableplus.com/
--
-- Database: webdb
-- Generation Time: 2022-10-10 23:07:44.3360
-- -------------------------------------------------------------


DROP TABLE IF EXISTS "public"."tbl_userrolesbyuser";
-- This script only contains the table creation statements and does not fully represent the table in the database. It's still missing: indices, triggers. Do not use it as a backup.

-- Table Definition
CREATE TABLE "public"."tbl_userrolesbyuser" (
    "userid" text NOT NULL,
    "roleid" text NOT NULL,
    "ownedutcdate" timestamp DEFAULT now(),
    PRIMARY KEY ("userid","roleid")
);


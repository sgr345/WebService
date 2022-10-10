-- -------------------------------------------------------------
-- TablePlus 5.0.0(454)
--
-- https://tableplus.com/
--
-- Database: webdb
-- Generation Time: 2022-10-10 23:07:55.6190
-- -------------------------------------------------------------


DROP TABLE IF EXISTS "public"."tbl_user";
-- This script only contains the table creation statements and does not fully represent the table in the database. It's still missing: indices, triggers. Do not use it as a backup.

-- Table Definition
CREATE TABLE "public"."tbl_user" (
    "userid" text NOT NULL,
    "username" text,
    "useremail" text,
    "guidsalt" text,
    "rngsalt" text,
    "passwordhash" text,
    "accessfailedcount" int4 DEFAULT 0,
    "ismembershipwithdrawn" bool DEFAULT true,
    "joinedutcdate" timestamp DEFAULT now(),
    "rf_token" text,
    PRIMARY KEY ("userid")
);


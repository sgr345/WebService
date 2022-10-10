-- -------------------------------------------------------------
-- TablePlus 5.0.0(454)
--
-- https://tableplus.com/
--
-- Database: webdb
-- Generation Time: 2022-10-10 23:08:17.3010
-- -------------------------------------------------------------


DROP TABLE IF EXISTS "public"."tbl_board";
-- This script only contains the table creation statements and does not fully represent the table in the database. It's still missing: indices, triggers. Do not use it as a backup.

-- Sequence and defined type
CREATE SEQUENCE IF NOT EXISTS tbl_board_no_seq;

-- Table Definition
CREATE TABLE "public"."tbl_board" (
    "no" int4 NOT NULL DEFAULT nextval('tbl_board_no_seq'::regclass),
    "title" text NOT NULL,
    "content" text NOT NULL,
    "readcount" int4 NOT NULL DEFAULT 0,
    "userid" text NOT NULL,
    "username" text NOT NULL,
    "createdat" timestamp NOT NULL DEFAULT now(),
    "updatedat" timestamp,
    PRIMARY KEY ("no")
);


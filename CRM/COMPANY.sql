/*
 Navicat SQLite Data Transfer

 Source Server         : data
 Source Server Type    : SQLite
 Source Server Version : 3035005 (3.35.5)
 Source Schema         : main

 Target Server Type    : SQLite
 Target Server Version : 3035005 (3.35.5)
 File Encoding         : 65001

 Date: 05/07/2023 17:51:16
*/

PRAGMA foreign_keys = false;

-- ----------------------------
-- Table structure for COMPANY
-- ----------------------------
DROP TABLE IF EXISTS "COMPANY";
CREATE TABLE "COMPANY" (
  "ID" INT NOT NULL,
  "NAME" TEXT NOT NULL,
  "AGE" INT NOT NULL,
  "ADDRESS" CHAR(50),
  "SALARY" REAL,
  PRIMARY KEY ("ID")
);

PRAGMA foreign_keys = true;

/*
 Navicat SQLite Data Transfer

 Source Server         : data
 Source Server Type    : SQLite
 Source Server Version : 3035005 (3.35.5)
 Source Schema         : main

 Target Server Type    : SQLite
 Target Server Version : 3035005 (3.35.5)
 File Encoding         : 65001

 Date: 05/07/2023 17:56:24
*/

PRAGMA foreign_keys = false;

-- ----------------------------
-- Table structure for Admin
-- ----------------------------
DROP TABLE IF EXISTS "Admin";
CREATE TABLE "Admin" (
  "ID" CHAR(50) NOT NULL,
  "AdminName" CHAR(50),
  "Tel" CHAR(50),
  "LoginName" CHAR(50),
  "LoginPwd" CHAR(50),
  "Rights" CHAR(50),
  "CreateTime" CHAR(50),
  "Active" CHAR(10),
  "Remark" CHAR(50),
  PRIMARY KEY ("ID")
);

PRAGMA foreign_keys = true;

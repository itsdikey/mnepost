BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "items" (
	"id"	INTEGER NOT NULL,
	"tracking_number"	TEXT NOT NULL,
	"title"	TEXT DEFAULT 'Package',
	"last_tracking_info"	TEXT,
	"is_archived"	INTEGER NOT NULL DEFAULT 0,
	PRIMARY KEY("id" AUTOINCREMENT)
);
COMMIT;

-- Copy and paste all this and execute it as a query using DB Browser for SQLite
CREATE TABLE "channel" (
	"channel_id"	INTEGER NOT NULL UNIQUE,
	"name"	TEXT NOT NULL,
	PRIMARY KEY("channel_id" AUTOINCREMENT)
);

CREATE TABLE "video" (
	"video_id"	INTEGER NOT NULL UNIQUE,
	"channel_id"	INTEGER NOT NULL,
	"title"	TEXT NOT NULL,
	"yt_video_id"	TEXT NOT NULL,
	"upload_date"	INTEGER NOT NULL,
	PRIMARY KEY("video_id" AUTOINCREMENT),
	FOREIGN KEY("channel_id") REFERENCES "channel"("channel_id")
);

CREATE TABLE "caption_type" (
	"caption_type_id"	INTEGER NOT NULL UNIQUE,
	"name"	TEXT NOT NULL,
	PRIMARY KEY("caption_type_id" AUTOINCREMENT)
);

CREATE TABLE "caption" (
	"caption_id"	INTEGER NOT NULL UNIQUE,
	"video_id"	INTEGER NOT NULL,
	"caption_type_id"	INTEGER NOT NULL,
	"start_seconds"	INTEGER NOT NULL,
	"caption_text"	TEXT NOT NULL,
	PRIMARY KEY("caption_id" AUTOINCREMENT),
	FOREIGN KEY("caption_type_id") REFERENCES "caption_type"("caption_type_id"),
	FOREIGN KEY("video_id") REFERENCES "video"("video_id")
);

CREATE VIEW vw_caption AS
SELECT 
	caption_id, 
	caption_type_id,
	caption_text,
	start_seconds,
	v.video_id,
	v.yt_video_id,
	v.title,
	v.channel_id,
	v.upload_date
FROM
    caption c
    INNER JOIN video v ON v.video_id = c.video_id;

-- Now set up virtual table to support full text search
-- and triggers to keep the virtual table in sync with the real table.
-- Shoutout: https://www.youtube.com/watch?v=eXMA_2dEMO0

CREATE VIRTUAL TABLE "caption_fts" USING fts5(caption_text, content='caption', content_rowid='caption_id');

CREATE TRIGGER insert_caption_fts
  after insert on caption
begin
  INSERT INTO caption_fts(rowid, caption_text)
  VALUES(NEW.caption_id, NEW.caption_text);
end;

CREATE TRIGGER update_caption_fts
  after update on caption
begin
  INSERT INTO caption_fts(caption_fts, rowid, caption_text) VALUES('delete', OLD.caption_id, OLD.caption_text);
  INSERT INTO caption_fts(rowid, caption_text) VALUES (NEW.caption_id, NEW.caption_text);
end;

CREATE TRIGGER delete_caption_fts
  after delete on caption
begin
  INSERT INTO caption_fts(caption_fts, rowid, caption_text) VALUES('delete', OLD.caption_id, OLD.caption_text);
end;
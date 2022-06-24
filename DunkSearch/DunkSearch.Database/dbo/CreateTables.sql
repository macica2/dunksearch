-- Run this script to create all the tables the database needs
CREATE TABLE channel
( 
	channel_id INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
	name TEXT NOT NULL,
	yt_channel_id TEXT NOT NULL
);

CREATE TABLE video
(
	video_id INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
	channel_id INTEGER REFERENCES channel(channel_id) NOT NULL,
	title TEXT NOT NULL,
	yt_video_id TEXT NOT NULL,
	upload_date DATE NOT NULL
);

CREATE TABLE caption_type
(
	caption_type_id INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
	name TEXT NOT NULL
);

CREATE TABLE caption
(
	caption_id INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
	video_id INTEGER REFERENCES video(video_id) NOT NULL,
	caption_type_id INTEGER REFERENCES caption_type(caption_type_id) NOT NULL,
	start_seconds INTEGER NOT NULL,
	caption_text TEXT NOT NULL
);

/* Add generated tsvector column which we will search and index on */
ALTER TABLE caption
ADD COLUMN caption_text_vector tsvector
GENERATED ALWAYS AS (to_tsvector('english', coalesce(caption_text, ''))) STORED;

/* Add index for speeding up searches */
CREATE INDEX ON caption USING GIST (caption_text_vector);

CREATE TABLE app_event_log
(
	app_event_log_id INTEGER GENERATED ALWAYS AS IDENTIY PRIMARY KEY,
	event_type TEXT NOT NULL,
	event_details TEXT NULL,
	ip_address INET NULL
	create_date TIMESTAMP NOT NULL
);

CREATE TABLE unsupported_video
(
	unsupported_video_id INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
	yt_video_id TEXT NOT NULL,
	title TEXT NOT NULL,
	reason TEXT NOT NULL
);
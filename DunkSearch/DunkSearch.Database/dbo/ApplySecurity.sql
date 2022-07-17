-- Use this to grant the app user account access.
-- Replace the word 'username' with the actual username.
-- Replace the word 'databasename' with the actual DB name.
GRANT CONNECT ON DATABASE databasename TO username;
GRANT SELECT ON channel TO username;
GRANT SELECT ON video TO username;
GRANT SELECT ON caption TO username;
GRANT SELECT ON caption_type TO username;
GRANT SELECT ON unsupported_video TO username;
GRANT SELECT, INSERT ON app_event_log TO username;
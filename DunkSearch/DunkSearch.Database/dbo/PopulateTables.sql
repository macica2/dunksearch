﻿-- Create the Channel records --
INSERT INTO channel(name, yt_channel_id)
VALUES 
	('videogamedunkey', 'UCsvn_Po0SmunchJYOWpOxMg'),
	('WaLter .NO (coming soon)', 'UCbOPMX9iWXlulmbNXrz6oLw'),
	('Dunk Tank (coming soon)', 'UCGiJeCKTVKIxtaYZOidh19g'),
	('Leahbee (coming soon)', 'UCq7D2jqBfjse5M18CaLlTjA');

-- Create the CaptionType records --
INSERT INTO caption_type(name)
VALUES 
	('English (auto-generated)'),
	('English');

-- Unsupported Videos (as of 6/21/2022 for VGD, 7/16/22 for Leahbee)
-- REPLACE the channel ID with the ID of the channel in after creating those records
INSERT INTO unsupported_video(channel_id, yt_video_id, title, reason)
VALUES
	(dunkey_channel_id, 'Wqsb7VR6EB8', 'LoL : Ashe''s Revenge', 'Music only'),
	(dunkey_channel_id, '8MhOouhhrTU', 'Black Ops : Epic Boner Jamz', 'Music only'),
	(dunkey_channel_id, 'uZfJ5S3WCY4', 'Modern Trollfare', 'No captions'),
	(dunkey_channel_id, 'o7HUbFZW_kU', 'League of Legends : AFK Troller Strategy', 'Music only'),
	(dunkey_channel_id, '3S8BvTbhkY0', 'Crysis 2 - Console Version (dunkview)', 'No captions'),
	(dunkey_channel_id, 'RM9R4IFSMHQ', 'Falcom Chip !', 'No captions'),
	(dunkey_channel_id, '-uo-JnbtVqo', 'League of Legends : Epic Escape', 'Music only'),
	(dunkey_channel_id, 'yQKI9oclZsk', 'League of Legends : OOO MAMA', 'No captions'),
	(dunkey_channel_id, 'Ong43KWh_x8', 'League of Legends : Ranked', 'Music only'),
	(dunkey_channel_id, 'gDHMtw_ECjE', 'League of Legends : Dream Team', 'No captions'),
	(dunkey_channel_id, '8qnQNe9zQmo', 'Skyman and the Boosher Mover', 'No captions'),
	(dunkey_channel_id, 'wWBYZQwmJCg', 'Beast Mode Counter Strike Pwn Town', 'Music only'),
	(dunkey_channel_id, 'zIoJndp0sKI', 'League of Legends : Renekton''s Alt. Lore', 'No captions'),
	(dunkey_channel_id, 'kvwmNx5AeAs', 'League of Legends : Weo Weo Weo', 'Just noises'),
	(dunkey_channel_id, 'PIJ54HCj_mY', 'LoL Cypher - Dunkey (calling out Protoman)', 'No captions'),
	(dunkey_channel_id, 'Mj5KiiHznJk', 'League of Legends : Wondershot Ashe', 'Music only'),
	(dunkey_channel_id, '7pOAsXK3bYs', 'League of Legends : Five Man Jungle', 'No captions'),
	(dunkey_channel_id, 'YB_-T_dW2Aw', 'League of Legends : Foxstar 4D', 'No captions'),
	(dunkey_channel_id, 'JUFl_sDOHlM', 'League of Legends : Shoot Boys', 'No captions'),
	(dunkey_channel_id, 'zPej5c7DU7Q', 'League of Legends : Fizz Glitch', 'No captions'),
	(dunkey_channel_id, '2YeTbQYF6Hk', 'League of Legends : Varus is Out', 'No captions'),
	(dunkey_channel_id, 'aYDIYzPFvKc', 'Diablo 3 : Inside Look', 'No captions'),
	(dunkey_channel_id, 'LmjHx7yIvV8', 'League of Legends : Meet the Sin', 'No captions'),
	(dunkey_channel_id, 'VS9IhcD1YYE', 'League of Legends : Downright Dyruis', 'No captions'),
	(dunkey_channel_id, '17XvXLHFDP0', 'League of Legends : Impressions', 'No captions'),
	(dunkey_channel_id, '6r5sMN_QGQs', 'Dark Knight Rises is Sold Out', 'No captions'),
	(dunkey_channel_id, 'w5_cVzKFyTk', 'League of Legends : Weo Weo Caitlyn', 'No captions'),
	(dunkey_channel_id, 'Jkg8GiriHvU', 'League of Legends : Riot Points Hack Get 60k RP', 'No captions'),
	(dunkey_channel_id, 'iUejvXJ9-II', 'League of Legends : The Movie', 'No captions'),
	(dunkey_channel_id, 'rw2sMk-PTp8', 'Super Mario 64 (Ep. 18) : The Return', 'No captions'),
	(dunkey_channel_id, 'M_avgJJvgSc', 'League of Legends : The Lion Book', 'No captions'),
	(dunkey_channel_id, 'qdroeDSpoGU', 'League of Legends : Team All Mid', 'No captions'),
	(dunkey_channel_id, 'WEtf3XolX9w', 'League of Legends : Slam House', 'No captions'),
	(dunkey_channel_id, 'rCsvWfEMcII', 'League of Legends : Swat Recon', 'No captions'),
	(dunkey_channel_id, 'ECQKwvEoD8o', 'Phone Men', 'No captions'),
	(dunkey_channel_id, 'c79sW3gVp8s', 'League of Legends : Gentlemen''s Tribunal', 'No captions'),
	(dunkey_channel_id, 't88VU72eAI0', 'League of Legends : New Rengar Build', 'No captions'),
	(dunkey_channel_id, 'bxKSMQ7upHg', 'Super Mario 64 (Ep. 19) : Wiggler''s Red Coins', 'No captions'),
	(dunkey_channel_id, 'GGEGF7cHmMU', 'Harlem Shake', 'Music only'),
	(dunkey_channel_id, 'kacTMmoQPQM', 'Dunkey Die', 'Music only'),
	(dunkey_channel_id, 'kdGyUvCksD8', 'League of Legends : Bird Bitch', 'No captions'),
	(dunkey_channel_id, 'sdnYy1b8KEY', 'League of Legends : Die Sounds', 'Just noises'),
	(dunkey_channel_id, 'kCHVdq73o4s', 'League of Legends : Dubstep Singed', 'No captions'),
	(dunkey_channel_id, 'EC-VrN9ABkk', 'League of Legends : Beo Beo Trungle', 'No captions'),
	(dunkey_channel_id, '4O4SRcNBGp8', 'League of Legends : Jinx', 'Just noises'),
	(dunkey_channel_id, 'AaxobrC3Qmo', 'League of Legends : Hell Jam', 'No captions'),
	(dunkey_channel_id, 'PttKq0GcnoQ', 'Soccer', 'Music only'),
	(dunkey_channel_id, 'YpzV6h6xiZI', 'League of Legends : Urf Rap', 'No captions'),
	(dunkey_channel_id, 'Bl4X8bCVnQU', 'Dunkey''s 1 Million Subs Dance Hoedown', 'Music only'),
	(dunkey_channel_id, 'EX9aDLjKYww', 'Relaxing Space Safari', 'Music only'),
	(dunkey_channel_id, 'YgSSql6MAKo', 'Dunkey Dance', 'Music only'),
	(dunkey_channel_id, '6n83K-cHHg4', 'Youtubers', 'Just noises'),
	(dunkey_channel_id, 'pKYeAN-_wFI', 'Whiplash and La La Land', 'No captions'),
	(dunkey_channel_id, 'waXb8QGdEYQ', 'Dunkey and Leah''s Wedding', 'No captions (mostly music)'),
	(dunkey_channel_id, 'aaLiLRVeaZA', 'Metal Gear Solid Explained', 'No captions'),
	(dunkey_channel_id, 'n0qIgPIDy00', 'funny birds have a sword fight with their beaks ROFL', 'Music only'),
	(dunkey_channel_id, 'fXSjsSHWekw', 'Maneater', 'No captions'),
	(dunkey_channel_id, 'dW0QCU3RPtY', 'Worms', 'No captions'),
	(dunkey_channel_id, 'XZp6zxh2yjE', 'The Sopranos for PS2', 'No captions'),
	(dunkey_channel_id, 'iGOMViJlQmE', 'Reacting to the FAKEST GAMER EVER !?', 'No captions'),
	(dunkey_channel_id, 'TVVum6X3TuM', 'More Dunkey More Problems', 'No captions'),
	(dunkey_channel_id, 'iTBW6jbHgX0', 'Playtime', 'No captions'),
	(dunkey_channel_id, 'ajh9e3PDAOc', 'Battlefield 2077', 'Music and noises only'),
	(dunkey_channel_id, 'KuvDsT4sRzU', 'Donkey Kong December', 'No captions'),
	(dunkey_channel_id, 'DRnMwtdegII', 'Super Donkey Kong Shake', 'No captions'),
	(dunkey_channel_id, 'wXqHtF4I3M8', '100 Movie Recommendations in One Minute', 'No audio'),
	(dunkey_channel_id, 'o2TdptvwVKQ', 'Franken RPG', 'No captions'),
	(dunkey_channel_id, 'IiMJzjKKtgg', 'Dunkey''s Epic 30k Dunder Dance Down', 'Music only'),
	(dunkey_channel_id, 'RDcWc1t9XN8', 'League of Legends : Bouncy Time', 'No captions'),
	(dunkey_channel_id, 'mGLMi9kXTRI', 'The Next Smash Bros Roster', 'No captions'),
	(leahbee_channel_id, 'ouVgKOPnVDc', 'frog', 'No audio'),
	(leahbee_channel_id, 'rAzvFOolo0A', 'hamster', 'No audio'),
	(leahbee_channel_id, 'sBRNelMnr4c', 'mimi', 'Music only'),
	(leahbee_channel_id, 'AN2Kv_8e2VY', 'jason and his posse', 'No talking'),
	(leahbee_channel_id, 'l1ProvUrUeg', 'disnee time', 'Music only'),
	(leahbee_channel_id, 'UtThTcROWwY', 'more disney!', 'No captions (mostly music)'),
	(leahbee_channel_id, 'ENdMy_cy67g', 'hamster christmas', 'Music only'),
	(leahbee_channel_id, 'htiVQ-SKGWA', 'mimi''s 1st time outside!', 'Music only'),
	(leahbee_channel_id, 'lPb0-e7Ndsg', 'mimi enjoying the weather :)', 'Music only'),
	(leahbee_channel_id, 'tjKP0HJ4tsg', 'my magical fairy garden!', 'Music only'),
	(leahbee_channel_id, 'MI0hZNBWzDU', 'universal pt. 1!', 'No captions (mostly music)'),
	(leahbee_channel_id, '9BTwVAWJlcc', 'universal pt. 2!!', 'Music only'),
	(leahbee_channel_id, '9663nNV1OAE', 'speed painting rainbow trout', 'Music only'),
	(leahbee_channel_id, 'UJZxbFq70VU', 'polymer clay (braum) poro!', 'Music only'),
	(leahbee_channel_id, '67CFn-PXtOw', 'clay lulu polymorphs & mini dunkey', 'Music only'),
	(leahbee_channel_id, 'NEFiSxZPqYw', 'pax east 2016!', 'No captions (mostly music)'),
	(leahbee_channel_id, 'RxnedjB4YHU', 'jugglin bubbles ?', 'No captions'),
	(leahbee_channel_id, 'xgf5ctePmIA', 'mario makers !', 'No captions'),
	(leahbee_channel_id, '1MtMQnEZW2g', 'birthday trip to disneyworld!', 'No captions'),
	(leahbee_channel_id, 'jPJlXq3Ag9A', 'playing your mario levels! 1/2', 'No captions'),
	(leahbee_channel_id, 'DRJpEpYzfpg', 'playing your mario levels! 2/2', 'No captions'),
	(leahbee_channel_id, '0MLSC97Z1iE', 'POP the PIG board game!', 'No captions'),
	(leahbee_channel_id, 'Ge9xPeyqo7Q', 'pax west 2016!', 'No captions'),
	(leahbee_channel_id, 'dpTP0nf-5tg', 'seattle & pax west 2017 vlog!', 'No captions');
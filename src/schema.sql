CREATE TABLE PoliceCase (
	id integer PRIMARY KEY AUTOINCREMENT,
	case_type varchar(12) NOT NULL,
	start_date text(10) NOT NULL,
	end_date text(10)
);

CREATE TABLE Address (
	id integer PRIMARY KEY AUTOINCREMENT,
	line1 varchar(32) NOT NULL,
	line2 varchar(32),
	town varchar(40) NOT NULL,
	county varchar(32) NOT NULL,
	postcode varchar(10) NOT NULL
);

CREATE TABLE PersonOfInterest (
	id integer PRIMARY KEY AUTOINCREMENT,
	name varchar(50),
	date_of_birth text(10),
	current_location varchar(20),
	facebook_account varchar(50),
	twitter_account varchar(50),
	address_id integer,
	case_id integer,
	FOREIGN KEY(address_id) REFERENCES Address(id),
	FOREIGN KEY(case_id) REFERENCES PoliceCase(id)
);

CREATE TABLE Contact (
	id integer PRIMARY KEY AUTOINCREMENT,
	phone_number varchar(15) NOT NULL,
	email_address varchar(32) NOT NULL,
	address_id varchar(32) NOT NULL,
	FOREIGN KEY(address_id) REFERENCES Address(id)
);

CREATE TABLE ContactPoIRelationship (
	contact_id integer,
	poi_id integer,
	FOREIGN KEY(contact_id) REFERENCES Contact(id),
	FOREIGN KEY(poi_id) REFERENCES PersonOfInterest(id)
);

CREATE TABLE Report (
	id integer PRIMARY KEY AUTOINCREMENT,
	report_text varchar(400) NOT NULL,
	filed_date text(10) NOT NULL,
	case_id integer NOT NULL,
	contact_id integer NOT NULL,
	FOREIGN KEY(case_id) REFERENCES PoliceCase(id),
	FOREIGN KEY(contact_id) REFERENCES Contact(id)
);

CREATE TABLE NewsArticles (
    case_id integer,
    headline varchar(32) NOT NULL,
    url varchar(40) NOT NULL,
    FOREIGN KEY(case_id) REFERENCES PoliceCase(id)
);

CREATE TABLE SocialMediaPosts (
    case_id integer,
    content varchar(800) NOT NULL,
    url varchar(50) NOT NULL,
    FOREIGN KEY(case_id) REFERENCES PoliceCase(id)
);

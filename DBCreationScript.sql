CREATE TABLE [Compound] (
  [CompoundID] int identity(1,1) not null,
  [CompoundName] varchar(100),
  [MolecularMass] decimal(18,18),
  PRIMARY KEY ([CompoundID])
);

CREATE TABLE [ResultsType] (
  [ResultsTypeID] int identity(1,1) not null,
  [Desc] varchar(20),
  PRIMARY KEY ([ResultsTypeID])
);

insert into ResultsType values ('Quanititative'), ('Qualitative')

CREATE TABLE [Work Order] (
  [OrderNumber] int identity(1,1) not null,
  [OrderTypeID] int,
  [CustomerID] int,
  [QuoteDate] date,
  [OrderDate] date,
  [StatusID] int,
  [Comments] varchar(500),
  PRIMARY KEY ([OrderNumber])
);


CREATE TABLE [ResultFile] (

  [ResultFileID] int identity(1,1) not null,
  [TestID] int,
  [ResultsTypeID] int,
  [FileLocation] varchar(100),
  PRIMARY KEY ([ResultFileID])
);


CREATE TABLE [Sample] (
  [SampleID] int identity(1,1) not null,
  [CompoundID] int,
  [ReportedQty] decimal (18,2),
  [MeasuredQty] decimal (18,2),
  [MTD] decimal (18,2),
  [DateArrived] date,
  [ReceivedBy] int,
  [DueDate] date,
  [AppearanceID] int,
  [ReceivingNotes] varchar(500),
  PRIMARY KEY ([SampleID])
);

CREATE TABLE [OrderSample] (
  [SampleID] int,
  [OrderNumber] int,
);


CREATE TABLE [Test] (
  [TestID] int identity(1,1) not null,
  [OrderNumber] int,
  [SampleID] int,
  [TestTypeID] int,
  [ScheduledDate] date,
  [CompletedDate] date,
  [EmployeeID] int,
  PRIMARY KEY ([TestID])
);


CREATE TABLE [TestType] (
  [TestTypeID] int identity(1,1) not null,
  [TestName] varchar(100),
  [Desc] varchar(500),
  [Price] decimal(18,2),
  PRIMARY KEY ([TestTypeID])
);

CREATE TABLE [AssayTest] (
  [TestTypeID] int,
  [AssayID] int,
);


CREATE TABLE [Assay] (
  [AssayID] int identity(1,1) not null,
  [AssayName] varchar(50),
  [Desc] varchar(500),
  PRIMARY KEY ([AssayID])
);

CREATE TABLE [Customer] (
  [CustomerID] int identity(1,1) not null,
  [FirstName] varchar(50),
  [LastName] varchar(50),
  [Address] varchar(50),
  [City] varchar(50),
  [StateID] int,
  [CountryID] int,
  [PostalCode] varchar(5),
  [Email] varchar(50),
  [Phone] varchar(10),
  PRIMARY KEY ([CustomerID])
);

CREATE TABLE [Employee] (
  [EmployeeID] int identity(1,1) not null,
  [FirstName] varchar(50),
  [LastName] varchar(50),
  [WageTypeID] int,
  [Wage] decimal(18,2),
  [Address] varchar(50),
  [City] varchar(50),
  [StateID] int,
  [CountryID] int,
  [PostalCode] varchar(5),
  [Email] varchar(50),
  [Phone] varchar(10),
  [RoleID] int,
  [LocationID] int,
  PRIMARY KEY ([EmployeeID])
);


CREATE TABLE [EmployeeRole] (
  [RoleID] int identity(1,1) not null,
  [RoleName] varchar(50),
  [RoleDesc] varchar(500),
  PRIMARY KEY ([RoleID])
);

CREATE TABLE [Location] (
  [LocationID] int identity(1,1) not null,
  [LocationName] varchar(50),
  [Address] varchar(50),
  [City] varchar(50),
  [StateID] int,
  [CountryID] int,
  [PostalCode] varchar(5),
  [Phone] varchar(10),
  [Email] varchar(50),
  PRIMARY KEY ([LocationID])
);


CREATE TABLE [Country] (
  [CountryID] int identity(1,1) not null,
  [CountryAbbrev] char(2),
  [CountryName] varchar(50),
  PRIMARY KEY ([CountryID])
);

INSERT INTO Country VALUES ('AF', 'Afghanistan');
INSERT INTO Country VALUES ('AL', 'Albania');
INSERT INTO Country VALUES ('DZ', 'Algeria');
INSERT INTO Country VALUES ('DS', 'American Samoa');
INSERT INTO Country VALUES ('AD', 'Andorra');
INSERT INTO Country VALUES ('AO', 'Angola');
INSERT INTO Country VALUES ('AI', 'Anguilla');
INSERT INTO Country VALUES ('AQ', 'Antarctica');
INSERT INTO Country VALUES ('AG', 'Antigua and Barbuda');
INSERT INTO Country VALUES ('AR', 'Argentina');
INSERT INTO Country VALUES ('AM', 'Armenia');
INSERT INTO Country VALUES ('AW', 'Aruba');
INSERT INTO Country VALUES ('AU', 'Australia');
INSERT INTO Country VALUES ('AT', 'Austria');
INSERT INTO Country VALUES ('AZ', 'Azerbaijan');
INSERT INTO Country VALUES ('BS', 'Bahamas');
INSERT INTO Country VALUES ('BH', 'Bahrain');
INSERT INTO Country VALUES ('BD', 'Bangladesh');
INSERT INTO Country VALUES ('BB', 'Barbados');
INSERT INTO Country VALUES ('BY', 'Belarus');
INSERT INTO Country VALUES ('BE', 'Belgium');
INSERT INTO Country VALUES ('BZ', 'Belize');
INSERT INTO Country VALUES ('BJ', 'Benin');
INSERT INTO Country VALUES ('BM', 'Bermuda');
INSERT INTO Country VALUES ('BT', 'Bhutan');
INSERT INTO Country VALUES ('BO', 'Bolivia');
INSERT INTO Country VALUES ('BA', 'Bosnia and Herzegovina');
INSERT INTO Country VALUES ('BW', 'Botswana');
INSERT INTO Country VALUES ('BV', 'Bouvet Island');
INSERT INTO Country VALUES ('BR', 'Brazil');
INSERT INTO Country VALUES ('IO', 'British Indian Ocean Territory');
INSERT INTO Country VALUES ('BN', 'Brunei Darussalam');
INSERT INTO Country VALUES ('BG', 'Bulgaria');
INSERT INTO Country VALUES ('BF', 'Burkina Faso');
INSERT INTO Country VALUES ('BI', 'Burundi');
INSERT INTO Country VALUES ('KH', 'Cambodia');
INSERT INTO Country VALUES ('CM', 'Cameroon');
INSERT INTO Country VALUES ('CA', 'Canada');
INSERT INTO Country VALUES ('CV', 'Cape Verde');
INSERT INTO Country VALUES ('KY', 'Cayman Islands');
INSERT INTO Country VALUES ('CF', 'Central African Republic');
INSERT INTO Country VALUES ('TD', 'Chad');
INSERT INTO Country VALUES ('CL', 'Chile');
INSERT INTO Country VALUES ('CN', 'China');
INSERT INTO Country VALUES ('CX', 'Christmas Island');
INSERT INTO Country VALUES ('CC', 'Cocos (Keeling) Islands');
INSERT INTO Country VALUES ('CO', 'Colombia');
INSERT INTO Country VALUES ('KM', 'Comoros');
INSERT INTO Country VALUES ('CG', 'Congo');
INSERT INTO Country VALUES ('CK', 'Cook Islands');
INSERT INTO Country VALUES ('CR', 'Costa Rica');
INSERT INTO Country VALUES ('HR', 'Croatia (Hrvatska)');
INSERT INTO Country VALUES ('CU', 'Cuba');
INSERT INTO Country VALUES ('CY', 'Cyprus');
INSERT INTO Country VALUES ('CZ', 'Czech Republic');
INSERT INTO Country VALUES ('DK', 'Denmark');
INSERT INTO Country VALUES ('DJ', 'Djibouti');
INSERT INTO Country VALUES ('DM', 'Dominica');
INSERT INTO Country VALUES ('DO', 'Dominican Republic');
INSERT INTO Country VALUES ('TP', 'East Timor');
INSERT INTO Country VALUES ('EC', 'Ecuador');
INSERT INTO Country VALUES ('EG', 'Egypt');
INSERT INTO Country VALUES ('SV', 'El Salvador');
INSERT INTO Country VALUES ('GQ', 'Equatorial Guinea');
INSERT INTO Country VALUES ('ER', 'Eritrea');
INSERT INTO Country VALUES ('EE', 'Estonia');
INSERT INTO Country VALUES ('ET', 'Ethiopia');
INSERT INTO Country VALUES ('FK', 'Falkland Islands (Malvinas)');
INSERT INTO Country VALUES ('FO', 'Faroe Islands');
INSERT INTO Country VALUES ('FJ', 'Fiji');
INSERT INTO Country VALUES ('FI', 'Finland');
INSERT INTO Country VALUES ('FR', 'France');
INSERT INTO Country VALUES ('FX', 'France, Metropolitan');
INSERT INTO Country VALUES ('GF', 'French Guiana');
INSERT INTO Country VALUES ('PF', 'French Polynesia');
INSERT INTO Country VALUES ('TF', 'French Southern Territories');
INSERT INTO Country VALUES ('GA', 'Gabon');
INSERT INTO Country VALUES ('GM', 'Gambia');
INSERT INTO Country VALUES ('GE', 'Georgia');
INSERT INTO Country VALUES ('DE', 'Germany');
INSERT INTO Country VALUES ('GH', 'Ghana');
INSERT INTO Country VALUES ('GI', 'Gibraltar');
INSERT INTO Country VALUES ('GK', 'Guernsey');
INSERT INTO Country VALUES ('GR', 'Greece');
INSERT INTO Country VALUES ('GL', 'Greenland');
INSERT INTO Country VALUES ('GD', 'Grenada');
INSERT INTO Country VALUES ('GP', 'Guadeloupe');
INSERT INTO Country VALUES ('GU', 'Guam');
INSERT INTO Country VALUES ('GT', 'Guatemala');
INSERT INTO Country VALUES ('GN', 'Guinea');
INSERT INTO Country VALUES ('GW', 'Guinea-Bissau');
INSERT INTO Country VALUES ('GY', 'Guyana');
INSERT INTO Country VALUES ('HT', 'Haiti');
INSERT INTO Country VALUES ('HM', 'Heard and Mc Donald Islands');
INSERT INTO Country VALUES ('HN', 'Honduras');
INSERT INTO Country VALUES ('HK', 'Hong Kong');
INSERT INTO Country VALUES ('HU', 'Hungary');
INSERT INTO Country VALUES ('IS', 'Iceland');
INSERT INTO Country VALUES ('IN', 'India');
INSERT INTO Country VALUES ('IM', 'Isle of Man');
INSERT INTO Country VALUES ('ID', 'Indonesia');
INSERT INTO Country VALUES ('IR', 'Iran (Islamic Republic of)');
INSERT INTO Country VALUES ('IQ', 'Iraq');
INSERT INTO Country VALUES ('IE', 'Ireland');
INSERT INTO Country VALUES ('IL', 'Israel');
INSERT INTO Country VALUES ('IT', 'Italy');
INSERT INTO Country VALUES ('CI', 'Ivory Coast');
INSERT INTO Country VALUES ('JE', 'Jersey');
INSERT INTO Country VALUES ('JM', 'Jamaica');
INSERT INTO Country VALUES ('JP', 'Japan');
INSERT INTO Country VALUES ('JO', 'Jordan');
INSERT INTO Country VALUES ('KZ', 'Kazakhstan');
INSERT INTO Country VALUES ('KE', 'Kenya');
INSERT INTO Country VALUES ('KI', 'Kiribati');
INSERT INTO Country VALUES ('KP', 'Korea, Democratic People''s Republic of');
INSERT INTO Country VALUES ('KR', 'Korea, Republic of');
INSERT INTO Country VALUES ('XK', 'Kosovo');
INSERT INTO Country VALUES ('KW', 'Kuwait');
INSERT INTO Country VALUES ('KG', 'Kyrgyzstan');
INSERT INTO Country VALUES ('LA', 'Lao People''s Democratic Republic');
INSERT INTO Country VALUES ('LV', 'Latvia');
INSERT INTO Country VALUES ('LB', 'Lebanon');
INSERT INTO Country VALUES ('LS', 'Lesotho');
INSERT INTO Country VALUES ('LR', 'Liberia');
INSERT INTO Country VALUES ('LY', 'Libyan Arab Jamahiriya');
INSERT INTO Country VALUES ('LI', 'Liechtenstein');
INSERT INTO Country VALUES ('LT', 'Lithuania');
INSERT INTO Country VALUES ('LU', 'Luxembourg');
INSERT INTO Country VALUES ('MO', 'Macau');
INSERT INTO Country VALUES ('MK', 'Macedonia');
INSERT INTO Country VALUES ('MG', 'Madagascar');
INSERT INTO Country VALUES ('MW', 'Malawi');
INSERT INTO Country VALUES ('MY', 'Malaysia');
INSERT INTO Country VALUES ('MV', 'Maldives');
INSERT INTO Country VALUES ('ML', 'Mali');
INSERT INTO Country VALUES ('MT', 'Malta');
INSERT INTO Country VALUES ('MH', 'Marshall Islands');
INSERT INTO Country VALUES ('MQ', 'Martinique');
INSERT INTO Country VALUES ('MR', 'Mauritania');
INSERT INTO Country VALUES ('MU', 'Mauritius');
INSERT INTO Country VALUES ('TY', 'Mayotte');
INSERT INTO Country VALUES ('MX', 'Mexico');
INSERT INTO Country VALUES ('FM', 'Micronesia, Federated States of');
INSERT INTO Country VALUES ('MD', 'Moldova, Republic of');
INSERT INTO Country VALUES ('MC', 'Monaco');
INSERT INTO Country VALUES ('MN', 'Mongolia');
INSERT INTO Country VALUES ('ME', 'Montenegro');
INSERT INTO Country VALUES ('MS', 'Montserrat');
INSERT INTO Country VALUES ('MA', 'Morocco');
INSERT INTO Country VALUES ('MZ', 'Mozambique');
INSERT INTO Country VALUES ('MM', 'Myanmar');
INSERT INTO Country VALUES ('NA', 'Namibia');
INSERT INTO Country VALUES ('NR', 'Nauru');
INSERT INTO Country VALUES ('NP', 'Nepal');
INSERT INTO Country VALUES ('NL', 'Netherlands');
INSERT INTO Country VALUES ('AN', 'Netherlands Antilles');
INSERT INTO Country VALUES ('NC', 'New Caledonia');
INSERT INTO Country VALUES ('NZ', 'New Zealand');
INSERT INTO Country VALUES ('NI', 'Nicaragua');
INSERT INTO Country VALUES ('NE', 'Niger');
INSERT INTO Country VALUES ('NG', 'Nigeria');
INSERT INTO Country VALUES ('NU', 'Niue');
INSERT INTO Country VALUES ('NF', 'Norfolk Island');
INSERT INTO Country VALUES ('MP', 'Northern Mariana Islands');
INSERT INTO Country VALUES ('NO', 'Norway');
INSERT INTO Country VALUES ('OM', 'Oman');
INSERT INTO Country VALUES ('PK', 'Pakistan');
INSERT INTO Country VALUES ('PW', 'Palau');
INSERT INTO Country VALUES ('PS', 'Palestine');
INSERT INTO Country VALUES ('PA', 'Panama');
INSERT INTO Country VALUES ('PG', 'Papua New Guinea');
INSERT INTO Country VALUES ('PY', 'Paraguay');
INSERT INTO Country VALUES ('PE', 'Peru');
INSERT INTO Country VALUES ('PH', 'Philippines');
INSERT INTO Country VALUES ('PN', 'Pitcairn');
INSERT INTO Country VALUES ('PL', 'Poland');
INSERT INTO Country VALUES ('PT', 'Portugal');
INSERT INTO Country VALUES ('PR', 'Puerto Rico');
INSERT INTO Country VALUES ('QA', 'Qatar');
INSERT INTO Country VALUES ('RE', 'Reunion');
INSERT INTO Country VALUES ('RO', 'Romania');
INSERT INTO Country VALUES ('RU', 'Russian Federation');
INSERT INTO Country VALUES ('RW', 'Rwanda');
INSERT INTO Country VALUES ('KN', 'Saint Kitts and Nevis');
INSERT INTO Country VALUES ('LC', 'Saint Lucia');
INSERT INTO Country VALUES ('VC', 'Saint Vincent and the Grenadines');
INSERT INTO Country VALUES ('WS', 'Samoa');
INSERT INTO Country VALUES ('SM', 'San Marino');
INSERT INTO Country VALUES ('ST', 'Sao Tome and Principe');
INSERT INTO Country VALUES ('SA', 'Saudi Arabia');
INSERT INTO Country VALUES ('SN', 'Senegal');
INSERT INTO Country VALUES ('RS', 'Serbia');
INSERT INTO Country VALUES ('SC', 'Seychelles');
INSERT INTO Country VALUES ('SL', 'Sierra Leone');
INSERT INTO Country VALUES ('SG', 'Singapore');
INSERT INTO Country VALUES ('SK', 'Slovakia');
INSERT INTO Country VALUES ('SI', 'Slovenia');
INSERT INTO Country VALUES ('SB', 'Solomon Islands');
INSERT INTO Country VALUES ('SO', 'Somalia');
INSERT INTO Country VALUES ('ZA', 'South Africa');
INSERT INTO Country VALUES ('GS', 'South Georgia South Sandwich Islands');
INSERT INTO Country VALUES ('ES', 'Spain');
INSERT INTO Country VALUES ('LK', 'Sri Lanka');
INSERT INTO Country VALUES ('SH', 'St. Helena');
INSERT INTO Country VALUES ('PM', 'St. Pierre and Miquelon');
INSERT INTO Country VALUES ('SD', 'Sudan');
INSERT INTO Country VALUES ('SR', 'Suriname');
INSERT INTO Country VALUES ('SJ', 'Svalbard and Jan Mayen Islands');
INSERT INTO Country VALUES ('SZ', 'Swaziland');
INSERT INTO Country VALUES ('SE', 'Sweden');
INSERT INTO Country VALUES ('CH', 'Switzerland');
INSERT INTO Country VALUES ('SY', 'Syrian Arab Republic');
INSERT INTO Country VALUES ('TW', 'Taiwan');
INSERT INTO Country VALUES ('TJ', 'Tajikistan');
INSERT INTO Country VALUES ('TZ', 'Tanzania, United Republic of');
INSERT INTO Country VALUES ('TH', 'Thailand');
INSERT INTO Country VALUES ('TG', 'Togo');
INSERT INTO Country VALUES ('TK', 'Tokelau');
INSERT INTO Country VALUES ('TO', 'Tonga');
INSERT INTO Country VALUES ('TT', 'Trinidad and Tobago');
INSERT INTO Country VALUES ('TN', 'Tunisia');
INSERT INTO Country VALUES ('TR', 'Turkey');
INSERT INTO Country VALUES ('TM', 'Turkmenistan');
INSERT INTO Country VALUES ('TC', 'Turks and Caicos Islands');
INSERT INTO Country VALUES ('TV', 'Tuvalu');
INSERT INTO Country VALUES ('UG', 'Uganda');
INSERT INTO Country VALUES ('UA', 'Ukraine');
INSERT INTO Country VALUES ('AE', 'United Arab Emirates');
INSERT INTO Country VALUES ('GB', 'United Kingdom');
INSERT INTO Country VALUES ('US', 'United States');
INSERT INTO Country VALUES ('UM', 'United States minor outlying islands');
INSERT INTO Country VALUES ('UY', 'Uruguay');
INSERT INTO Country VALUES ('UZ', 'Uzbekistan');
INSERT INTO Country VALUES ('VU', 'Vanuatu');
INSERT INTO Country VALUES ('VA', 'Vatican City State');
INSERT INTO Country VALUES ('VE', 'Venezuela');
INSERT INTO Country VALUES ('VN', 'Vietnam');
INSERT INTO Country VALUES ('VG', 'Virgin Islands (British)');
INSERT INTO Country VALUES ('VI', 'Virgin Islands (U.S.)');
INSERT INTO Country VALUES ('WF', 'Wallis and Futuna Islands');
INSERT INTO Country VALUES ('EH', 'Western Sahara');
INSERT INTO Country VALUES ('YE', 'Yemen');
INSERT INTO Country VALUES ('ZR', 'Zaire');
INSERT INTO Country VALUES ('ZM', 'Zambia');
INSERT INTO Country VALUES ('ZW', 'Zimbabwe');



CREATE TABLE [State] (
  [StateID] int identity(1,1) not null,
  [StateAbbrev] char(2),
  [StateName] varchar(50),
  [CountryID] int,
  PRIMARY KEY ([StateID])
);

INSERT INTO State (StateName, StateAbbrev, CountryID) VALUES
 ('Alaska', 'AK', 230),
 ('Alabama', 'AL', 230),
 ('American Samoa', 'AS', 230),
 ('Arizona', 'AZ', 230),
 ('Arkansas', 'AR', 230),
 ('California', 'CA', 230),
 ('Colorado', 'CO', 230),
 ('Connecticut', 'CT', 230),
 ('Delaware', 'DE', 230),
 ('District of Columbia', 'DC', 230),
 ('Florida', 'FL', 230),
 ('Georgia', 'GA', 230), 
 ('Guam', 'GU', 230),
 ('Hawaii', 'HI', 230),
 ('Idaho', 'ID', 230),
 ('Illinois', 'IL', 230),
 ('Indiana', 'IN', 230),
 ('Iowa', 'IA', 230),
 ('Kansas', 'KS', 230),
 ('Kentucky', 'KY', 230),
 ('Louisiana', 'LA', 230),
 ('Maine', 'ME', 230),
 ('Maryland', 'MD', 230),
 ('Massachusetts', 'MA', 230),
 ('Michigan', 'MI', 230),
 ('Minnesota', 'MN', 230),
 ('Mississippi', 'MS', 230),
 ('Missouri', 'MO', 230),
 ('Montana', 'MT', 230),
 ('Nebraska', 'NE', 230),
 ('Nevada', 'NV', 230),
 ('New Hampshire', 'NH', 230),
 ('New Jersey', 'NJ', 230),
 ('New Mexico', 'NM', 230), 
 ('New York', 'NY', 230),
 ('North Carolina', 'NC', 230),
 ('North Dakota', 'ND', 230),
 ('Northern Mariana Islands', 'MP', 230),
 ('Ohio', 'OH', 230),
 ('Oklahoma', 'OK', 230),
 ('Oregon', 'OR', 230),
 ('Palau', 'PW', 230), 
 ('Pennsylvania', 'PA', 230),
 ('Puerto Rico', 'PR', 230),
 ('Rhode Island', 'RI', 230),
 ('South Carolina', 'SC', 230),
 ('South Dakota', 'SD', 230),
 ('Tennessee', 'TN', 230),
 ('Texas', 'TX', 230), 
 ('Utah', 'UT', 230),
 ('Vermont', 'VT', 230),
 ('Virgin Islands', 'VI', 230),
 ('Virginia', 'VA', 230),
 ('Washington', 'WA', 230),
 ('West Virginia', 'WV', 230),
 ('Wisconsin', 'WI', 230), 
 ('Wyoming', 'WY', 230),
 ('Alberta', 'AB', 38),
	('British Columbia', 'BC', 38),
	('Manitoba', 'MB', 38),
	('New Brunswick', 'NB', 38),
	('Newfoundland and Labrador', 'NL', 38),
	('Northwest Territories', 'NT', 38),
	('Nova Scotia', 'NS', 38),
	('Nunavut', 'NU', 38),
	('Ontario', 'ON', 38),
	('Prince Edward Island', 'PE', 38),
	('Québec', 'QC', 38),
	('Saskatchewan', 'SK', 38),
	('Yukon Territory', 'YT', 38); 

CREATE TABLE [OrderType] (
  [OrderTypeID] int identity(1,1) not null,
  [OrderTypeDesc] varchar(50),
  PRIMARY KEY ([OrderTypeID])
);

CREATE TABLE [OrderStatus] (
  [StatusID] int identity(1,1) not null,
  [Status] varchar(50),
  PRIMARY KEY ([StatusID])
);

CREATE TABLE [Appearance] (
  [AppearanceID] int identity(1,1) not null,
  [Appearance] varchar(50),
  PRIMARY KEY ([AppearanceID])
);

CREATE TABLE [WageType] (
  [WageTypeID] int identity(1,1) not null,
  [WageType] varchar(50),
  PRIMARY KEY ([WageTypeID])
);


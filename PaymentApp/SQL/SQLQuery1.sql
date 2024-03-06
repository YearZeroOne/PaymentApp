use master
go

create database PaymentDb
go

use PaymentDb
go


create table Member(
Id int primary key identity,
FirstName varchar(100) not null,
LastName varchar(100) not null,
Image varchar(500) null,
IsActive bit not null

)


create table Payment(
Id int primary key identity,
Amount decimal(13,2) not null,
PaymentDate date not null,
Description varchar(50) not null,
InOut bit not null,
MemberId int not null references Member 
)


INSERT INTO Member(FirstName,LastName,Image,IsActive)
VALUES
  ('Holly','Lane','\MemberImages\Welcome Scan.jpg',1),
  ('Deirdre','Wolfe','\MemberImages\Welcome Scan.jpg',1),
  ('Pascale','Holder','\MemberImages\Welcome Scan.jpg',1),
  ('Keelie','Delgado','\MemberImages\Welcome Scan.jpg',0),
  ('Paula','Moss','\MemberImages\Welcome Scan.jpg',1),
  ('Elmo','Davidson','\MemberImages\Welcome Scan.jpg',0),
  ('Addison','Tillman','\MemberImages\Welcome Scan.jpg',0),
  ('Kirby','Santana','\MemberImages\Welcome Scan.jpg',1),
  ('Yvette','Brooks','\MemberImages\Welcome Scan.jpg',0),
  ('Trevor','Puckett','\MemberImages\Welcome Scan.jpg',1),
  ('Ariel','Conner','\MemberImages\Welcome Scan.jpg',0),
  ('Iris','Chandler','\MemberImages\Welcome Scan.jpg',1),
  ('Christopher','Richard','\MemberImages\Welcome Scan.jpg',1),
  ('Tanner','Barnett','\MemberImages\Welcome Scan.jpg',1),
  ('Axel','Mosley','\MemberImages\Welcome Scan.jpg',1),
  ('Cora','Silva','\MemberImages\Welcome Scan.jpg',1),
  ('Jordan','Hull','\MemberImages\Welcome Scan.jpg',1),
  ('Avram','Hardin','\MemberImages\Welcome Scan.jpg',0),
  ('Hadley','Petersen','\MemberImages\Welcome Scan.jpg',1),
  ('Quinn','Zamora','\MemberImages\Welcome Scan.jpg',0);

INSERT INTO Payment(Amount,PaymentDate,Description,InOut, MemberId)
Values
(76322.62, CONVERT(DATETIME, '2014-04-23', 120), 'SM5266117527753457659778253', 0, 10),
(27376.80, CONVERT(DATETIME, '2017-11-14', 120), 'DO93436339516497432608189184', 1, 6),
(98743.48, CONVERT(DATETIME, '2013-01-18', 120), 'TR767712480022630153217787', 0, 2),
(45093.78, CONVERT(DATETIME, '2020-01-21', 120), 'GI23SNMU596115041176826', 1, 18),
(64091.74, CONVERT(DATETIME, '2019-03-06', 120), 'MU0982476510598064953881356842', 0, 14),
(32862.88, CONVERT(DATETIME, '1996-02-08', 120), 'SE0959712953716332733698', 1, 10),
(2351.85, CONVERT(DATETIME, '2004-09-15', 120), 'EE574572742438733973', 0, 19),
(68195.79, CONVERT(DATETIME, '2027-06-25', 120), 'BE76687674135461', 0, 4),
(61220.77, CONVERT(DATETIME, '2012-03-19', 120), 'AZ55364042541735446244521645', 1, 5),
(24753.50, CONVERT(DATETIME, '2006-09-14', 120), 'GR2056315857736045128118621', 0, 13),
(76330.33, CONVERT(DATETIME, '2001-06-01', 120), 'BE23765261602172', 0, 14),
(4523.17, CONVERT(DATETIME, '2017-05-16', 120), 'SE3412943413451459621004', 1, 12),
(37338.14, CONVERT(DATETIME, '2009-07-07', 120), 'IL322331361863434352762', 1, 11),
(91735.09, CONVERT(DATETIME, '2003-08-05', 120), 'TR525453842647576663924724', 1, 6),
(21488.04, CONVERT(DATETIME, '2022-07-11', 120), 'MU2820645248434269258327644658', 1, 6),
(20336.16, CONVERT(DATETIME, '2022-05-14', 120), 'GR5434610283125143314153124', 0, 13),
(53083.82, CONVERT(DATETIME, '2021-01-11', 120), 'LI8470246711683765514', 0, 17),
(34973.79, CONVERT(DATETIME, '2029-03-02', 120), 'FI8446518155515274', 1, 1),
(39407.57, CONVERT(DATETIME, '2028-07-27', 120), 'SM7488116588337028125623578', 0, 13),
(61188.86, CONVERT(DATETIME, '1991-08-29', 120), 'FR4626282813032441667252807', 0, 11),
(92712.73, CONVERT(DATETIME, '1996-05-04', 120), 'FO3387769696344886', 0, 6),
(98348.09, CONVERT(DATETIME, '2011-09-28', 120), 'MK17280520393751572', 1, 4),
(82229.08, CONVERT(DATETIME, '2020-12-31', 120), 'GT21523157058784846421044876', 1, 3),
(60881.51, CONVERT(DATETIME, '2018-01-18', 120), 'BE04512266498267', 0, 2),
(16525.04, CONVERT(DATETIME, '2017-08-12', 120), 'NL72MKXF8906285874', 1, 14),
(21726.05, CONVERT(DATETIME, '2001-10-06', 120), 'HR5237682523624211013', 1, 8),
(23965.51, CONVERT(DATETIME, '1999-01-04', 120), 'FR9662917836217194235522366', 1, 1),
(7129.62, CONVERT(DATETIME, '2004-11-15', 120), 'FR5437287526368298718771953', 1, 8),
(21919.62, CONVERT(DATETIME, '2005-07-26', 120), 'GL6803414887482051', 1, 3),
(38206.06, CONVERT(DATETIME, '2004-06-20', 120), 'GT02484429439225280749918162', 1, 10),
(85411.26, CONVERT(DATETIME, '2014-04-12', 120), 'MK80777549741195771', 0, 18),
(26148.31, CONVERT(DATETIME, '2025-11-12', 120), 'KW6346841527288578148752061717', 1, 13),
(64989.84, CONVERT(DATETIME, '2007-05-07', 120), 'GE31511363684661731186', 1, 15),
(31438.63, CONVERT(DATETIME, '2017-08-26', 120), 'MK04782394714441896', 0, 19),
(30169.03, CONVERT(DATETIME, '2025-12-15', 120), 'ES1465205727161976748455', 0, 1),
(22644.52, CONVERT(DATETIME, '2013-08-30', 120), 'AT091399714563860153', 1, 17),
(87618.63, CONVERT(DATETIME, '2001-01-15', 120), 'AL53165645332817652632400396', 0, 10),
(94714.14, CONVERT(DATETIME, '2015-01-30', 120), 'CH4960157667826063158', 0, 1),
(78397.55, CONVERT(DATETIME, '2001-11-05', 120), 'RO47CNYS2454153441468625', 1, 15);
INSERT INTO Payment(Amount,PaymentDate,Description,InOut, MemberId)
Values
(44679.11, CONVERT(DATETIME, '2006-01-10', 120), 'IT875RIVOI73008797846372122', 1, 6),
(39515.66, CONVERT(DATETIME, '2002-10-25', 120), 'CR7826339866709211454', 1, 13),
(80090.79, CONVERT(DATETIME, '1991-10-24', 120), 'CR7803724654424788978', 1, 15),
(48837.02, CONVERT(DATETIME, '2007-08-11', 120), 'AT727029146650567322', 0, 9),
(82515.83, CONVERT(DATETIME, '2014-08-13', 120), 'DK6044963617358826', 1, 12),
(68001.29, CONVERT(DATETIME, '2024-04-16', 120), 'IE66DYDT23613101778727', 1, 10),
(10587.11, CONVERT(DATETIME, '2027-10-31', 120), 'EE306615262530219733', 0, 12),
(55285.14, CONVERT(DATETIME, '2019-08-24', 120), 'VG2328138258212312747325', 0, 8),
(51730.16, CONVERT(DATETIME, '2007-05-06', 120), 'CZ8645628064111972346738', 1, 15),
(71401.89, CONVERT(DATETIME, '2029-05-22', 120), 'GB03QLKW68553948614584', 0, 3),
(69014.53, CONVERT(DATETIME, '2021-07-02', 120), 'SA6899694358875886493302', 0, 8),
(55596.49, CONVERT(DATETIME, '1999-11-10', 120), 'CH3855406595185863376', 1, 8),
(32728.76, CONVERT(DATETIME, '1992-10-09', 120), 'LU795533391219250326', 1, 20),
(9588.75, CONVERT(DATETIME, '1991-01-23', 120), 'LB24180155550815888850488547', 0, 1),
(99400.21, CONVERT(DATETIME, '2012-03-16', 120), 'LU487567122857905367', 0, 2),
(17523.62, CONVERT(DATETIME, '2009-02-08', 120), 'PL78798689728241441802362077', 1, 3),
(67115.36, CONVERT(DATETIME, '2015-09-27', 120), 'GR7741642874481508899208825', 0, 5),
(85924.29, CONVERT(DATETIME, '2020-06-11', 120), 'LB14672327040806750542116717', 1, 6),
(84181.16, CONVERT(DATETIME, '2023-05-29', 120), 'FR3514818437571366721065443', 1, 2),
(13307.67, CONVERT(DATETIME, '1991-08-23', 120), 'DK5086503427237317', 0, 18),
(78084.32, CONVERT(DATETIME, '2000-01-27', 120), 'MR8821377532268464771241524', 0, 3),
(55835.35, CONVERT(DATETIME, '2013-10-07', 120), 'PL41356901415212755347817636', 0, 15),
(97039.82, CONVERT(DATETIME, '2018-05-01', 120), 'LT725255872630604585', 0, 1),
(9027.22, CONVERT(DATETIME, '2009-06-07', 120), 'CZ2637290341684736867564', 1, 6),
(94875.29, CONVERT(DATETIME, '1997-11-15', 120), 'IL401250682415525521954', 1, 18),
(16378.11, CONVERT(DATETIME, '2000-07-10', 120), 'HU26224444385779030275072255', 1, 9),
(19167.20, CONVERT(DATETIME, '2026-11-01', 120), 'GB41EMHJ47592242375455', 1, 7),
(45080.09, CONVERT(DATETIME, '1993-11-30', 120), 'GR5967579526701531756176743', 1, 15),
(62764.64, CONVERT(DATETIME, '2013-12-07', 120), 'SE4659662330472741559112', 0, 15),
(10787.58, CONVERT(DATETIME, '2008-01-25', 120), 'RS08142555677114522348', 0, 0),
(65553.03, CONVERT(DATETIME, '2013-10-14', 120), 'DO82511732211958532946662326', 0, 1),
(69693.06, CONVERT(DATETIME, '2007-11-08', 120), 'GB80ISPG32945858745346', 0, 16),
(45945.01, CONVERT(DATETIME, '2028-02-27', 120), 'BA340434240210741337', 1, 10),
(63958.16, CONVERT(DATETIME, '2025-10-03', 120), 'TN4461586914834868544547', 0, 15),
(80.01, CONVERT(DATETIME, '1998-09-14', 120), 'LU249462654296544365', 0, 2),
(94718.79, CONVERT(DATETIME, '2000-12-10', 120), 'MC1482796994600732688292766', 0, 19),
(72051.92, CONVERT(DATETIME, '1998-06-11', 120), 'AD9236742885539069609218', 0, 18),
(6182.89, CONVERT(DATETIME, '1999-04-16', 120), 'LV72LROV8948071564747', 0, 16),
(6057.24, CONVERT(DATETIME, '2018-07-05', 120), 'SM6411036263746523843915774', 1, 11);
INSERT INTO Payment(Amount,PaymentDate,Description,InOut, MemberId)
Values
(51265.71, CONVERT(DATETIME, '2016-03-13', 120), 'LB79758315275134521176649756', 1, 11),
(90605.14, CONVERT(DATETIME, '1997-02-17', 120), 'VG7898099164267958767697', 1, 9),
(96906.60, CONVERT(DATETIME, '2008-07-31', 120), 'PT97759274614273166647318', 1, 12),
(55839.86, CONVERT(DATETIME, '2028-08-01', 120), 'RO18ZRDI0246435192518843', 1, 11),
(36286.52, CONVERT(DATETIME, '2028-01-05', 120), 'EE584423712550428807', 0, 1),
(80314.06, CONVERT(DATETIME, '2026-09-24', 120), 'BG64TWKB07276348172353', 1, 4),
(30345.16, CONVERT(DATETIME, '1998-06-09', 120), 'EE498432068715424052', 1, 20),
(75712.91, CONVERT(DATETIME, '2025-03-15', 120), 'GB37NRNL10028330382412', 0, 1),
(80013.09, CONVERT(DATETIME, '2001-07-30', 120), 'IT331FHSVI74677261332393768', 1, 7),
(31563.82, CONVERT(DATETIME, '2023-08-16', 120), 'PT29167744851666151452640', 1, 7),
(55658.23, CONVERT(DATETIME, '2029-07-11', 120), 'CR8735866448183018369', 0, 9),
(43691.67, CONVERT(DATETIME, '2007-04-22', 120), 'BA771721385275816871', 0, 19),
(82865.95, CONVERT(DATETIME, '2012-08-15', 120), 'PL92352206067068732801233568', 1, 9),
(24893.57, CONVERT(DATETIME, '1992-07-10', 120), 'FR4939105577550389900838382', 1, 14),
(27145.19, CONVERT(DATETIME, '2005-07-18', 120), 'DE14318936272754438427', 1, 8),
(85151.09, CONVERT(DATETIME, '2002-04-22', 120), 'ME39686711666108245620', 0, 10),
(9745.16, CONVERT(DATETIME, '2000-02-25', 120), 'AT417886772384193544', 1, 17),
(52914.15, CONVERT(DATETIME, '1996-11-10', 120), 'CZ7755129322729224357174', 0, 16),
(99235.60, CONVERT(DATETIME, '2006-02-14', 120), 'PK2387442295035043047516', 1, 16),
(13289.73, CONVERT(DATETIME, '1996-11-10', 120), 'MK25327603381346746', 0, 19),
(2833.84, CONVERT(DATETIME, '2012-04-16', 120), 'GT37403551170082348794655418', 0, 7),
(53691.97, CONVERT(DATETIME, '2028-01-07', 120), 'AT360189835233781985', 0, 4),
(31544.67, CONVERT(DATETIME, '1998-07-16', 120), 'DK8882026644283276', 1, 11),
(15063.19, CONVERT(DATETIME, '2027-10-19', 120), 'NO0318181355521', 0, 19),
(41144.59, CONVERT(DATETIME, '2024-06-28', 120), 'TR504255039116030158687438', 0, 7),
(62744.06, CONVERT(DATETIME, '1999-07-19', 120), 'MD9850511767878755254025', 0, 18),
(74155.92, CONVERT(DATETIME, '2028-10-04', 120), 'AT574267768750485516', 0, 7),
(31784.47, CONVERT(DATETIME, '2028-12-02', 120), 'PL14274383534737651363185252', 0, 12),
(45442.94, CONVERT(DATETIME, '2000-06-21', 120), 'MR8374475885041733756377351', 1, 18),
(54347.80, CONVERT(DATETIME, '2019-08-21', 120), 'CY97067249654455733106138628', 1, 13),
(97029.89, CONVERT(DATETIME, '2003-10-07', 120), 'TR812370237684137380898786', 1, 4),
(68987.20, CONVERT(DATETIME, '2015-01-17', 120), 'LB26794221852116351312333098', 1, 4),
(38791.68, CONVERT(DATETIME, '1992-12-30', 120), 'NO6268284441352', 0, 5),
(74311.07, CONVERT(DATETIME, '2015-06-11', 120), 'GT37432060815014699927927312', 1, 13),
(15206.86, CONVERT(DATETIME, '2019-06-21', 120), 'GL2487368422948662', 0, 9),
(32055.42, CONVERT(DATETIME, '2022-04-20', 120), 'LT553879417680173808', 0, 14),
(33248.27, CONVERT(DATETIME, '2007-07-05', 120), 'CZ5076682636834124511052', 1, 14),
(42549.53, CONVERT(DATETIME, '2013-10-01', 120), 'LV92QXXC6127554155716', 1, 13),
(53312.22, CONVERT(DATETIME, '2012-03-13', 120), 'FR1951974641641621589427061', 0, 15);

INSERT INTO Payment(Amount,PaymentDate,Description,InOut, MemberId)
Values
(45809.23, CONVERT(DATETIME, '2005-10-20', 120), 'SA9055916937789668501418', 0, 7),
(40576.45, CONVERT(DATETIME, '2011-11-28', 120), 'TR281824891934686836445231', 0, 4),
(90480.55, CONVERT(DATETIME, '1998-11-26', 120), 'GT19004744103144521673653110', 0, 5),
(26295.31, CONVERT(DATETIME, '1995-11-02', 120), 'LV72USEQ3343738219468', 1, 17),
(8154.09, CONVERT(DATETIME, '2007-02-05', 120), 'FI0725975240862366', 1, 12),
(64969.67, CONVERT(DATETIME, '2014-07-31', 120), 'AE255371869661384638147', 0, 10),
(33342.40, CONVERT(DATETIME, '2019-07-19', 120), 'ES8782011142638562885656', 1, 13),
(68985.62, CONVERT(DATETIME, '2019-04-07', 120), 'MC5831347729892341053341736', 0, 17),
(51243.03, CONVERT(DATETIME, '2002-10-03', 120), 'ES8055413237862658831372', 0, 17),
(83475.91, CONVERT(DATETIME, '2012-02-02', 120), 'GR0916684348386371225455287', 1, 0),
(99558.47, CONVERT(DATETIME, '1995-10-29', 120), 'AE624699726341691075965', 1, 2),
(62195.74, CONVERT(DATETIME, '2017-02-22', 120), 'GI44YQHE311206178408334', 1, 13),
(71162.64, CONVERT(DATETIME, '1996-09-28', 120), 'IL595685226857684796451', 1, 3),
(81514.47, CONVERT(DATETIME, '1997-08-17', 120), 'SK7525485713806113398276', 1, 16),
(59419.93, CONVERT(DATETIME, '2010-01-26', 120), 'GT42637477096171228635702144', 0, 1),
(77334.17, CONVERT(DATETIME, '1993-05-06', 120), 'LI3254441158858272917', 0, 4),
(58237.96, CONVERT(DATETIME, '2027-03-14', 120), 'KW8423716660286742594547333482', 1, 8),
(34593.43, CONVERT(DATETIME, '2018-02-28', 120), 'IS352321773461607177208763', 0, 12),
(77593.52, CONVERT(DATETIME, '2019-08-19', 120), 'FR4180279126822811845311254', 0, 19),
(88905.91, CONVERT(DATETIME, '2002-12-23', 120), 'IT524LOKUI29621569464500844', 0, 8),
(33633.28, CONVERT(DATETIME, '2000-05-20', 120), 'SA5528425511332835851912', 1, 4),
(11425.88, CONVERT(DATETIME, '1997-03-31', 120), 'GR2133728188372254965614070', 0, 2),
(64661.02, CONVERT(DATETIME, '2009-12-21', 120), 'FR1875275106509116476513257', 1, 12),
(2746.56, CONVERT(DATETIME, '2021-04-11', 120), 'AE281237963864058279675', 1, 10),
(36780.88, CONVERT(DATETIME, '2001-11-19', 120), 'AZ50488198525312372208885688', 0, 9),
(3635.81, CONVERT(DATETIME, '1993-10-22', 120), 'PT77775824204621882345836', 0, 20),
(29154.95, CONVERT(DATETIME, '1995-03-23', 120), 'BE14521980204187', 1, 11),
(97134.97, CONVERT(DATETIME, '2003-01-29', 120), 'NO5512321873548', 0, 10),
(97473.98, CONVERT(DATETIME, '2015-03-21', 120), 'NL84KXVU4311977331', 0, 5),
(79476.40, CONVERT(DATETIME, '2018-02-02', 120), 'BG07SQXW86387933453613', 1, 15),
(54770.42, CONVERT(DATETIME, '1995-02-19', 120), 'CY42955768789985562571540771', 0, 0),
(4245.12, CONVERT(DATETIME, '2020-03-08', 120), 'GE35780671328783474478', 0, 8),
(52017.62, CONVERT(DATETIME, '2013-10-16', 120), 'RS61295945473586514421', 0, 19),
(12660.29, CONVERT(DATETIME, '2017-10-09', 120), 'SE2485693796564736341423', 0, 4),
(54214.25, CONVERT(DATETIME, '2014-05-18', 120), 'AL24895581224645347375852464', 0, 2),
(58833.31, CONVERT(DATETIME, '2012-04-11', 120), 'BA428261407763855168', 1, 18),
(21281.82, CONVERT(DATETIME, '2015-05-09', 120), 'GB55VFSI36437962792956', 1, 18),
(99385.85, CONVERT(DATETIME, '1994-01-24', 120), 'LB75206835859642412230980259', 0, 8);

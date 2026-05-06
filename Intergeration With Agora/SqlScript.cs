
//DECLARE @CourseId UNIQUEIDENTIFIER = NEWID();
//INSERT INTO Courses(Id, Title, Description, CreatedAt)
//VALUES(@CourseId, '.NET Core & Agora Masterclass', 'Learn how to build live streaming platforms.', GETDATE());


//DECLARE @InstructorId NVARCHAR(450) = 'INST-123-ABC';

//IF NOT EXISTS(SELECT 1 FROM AspNetUsers WHERE Id = @InstructorId)
//BEGIN
//    INSERT INTO AspNetUsers (
//        Id,
//		Discriminator,
//        UserName,
//        NormalizedUserName,
//        Email,
//        NormalizedEmail,
//        EmailConfirmed,
//        PasswordHash,
//        SecurityStamp,
//        ConcurrencyStamp,
//        PhoneNumberConfirmed,
//        TwoFactorEnabled,
//        LockoutEnabled,
//        AccessFailedCount -- العمود اللي عمل المشكلة

//    )
//    VALUES (
//        @InstructorId,
//	     'instructor',
//        'instructor@smartcourse.com',
//        'INSTRUCTOR@SMARTCOURSE.COM',
//        'instructor@smartcourse.com',
//        'INSTRUCTOR@SMARTCOURSE.COM',
//        1,
//        'AQAAAAEAACcQAAAAE...', -- هاش افتراضي
//        NEWID(),
//        NEWID(),
//        0, -- PhoneNumberConfirmed
//        0, -- TwoFactorEnabled
//        1, -- LockoutEnabled
//        0 -- AccessFailedCount (القيمة الافتراضية 0)

//    );
//END
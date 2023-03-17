USE [TATTER-FITNESS]
GO
/****** Object:  Table [dbo].[BodyMetricRecords]    Script Date: 3/17/2023 6:21:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BodyMetricRecords](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Weight] [float] NULL,
	[FatPercentage] [float] NULL,
	[Waist] [float] NULL,
	[Chest] [float] NULL,
	[Upperarms] [float] NULL,
	[Forearms] [float] NULL,
	[Shoulders] [float] NULL,
	[Hips] [float] NULL,
	[Thighs] [float] NULL,
	[Calves] [float] NULL,
	[Neck] [float] NULL,
	[Height] [int] NULL,
	[Date] [date] NOT NULL,
 CONSTRAINT [PK_BodyMetricRecords] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ExerciseModifiers]    Script Date: 3/17/2023 6:21:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExerciseModifiers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Sequence] [int] NOT NULL,
 CONSTRAINT [PK_ExerciseModifiers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Exercises]    Script Date: 3/17/2023 6:21:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Exercises](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[ExerciseTypeId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Exercises] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ExerciseTypes]    Script Date: 3/17/2023 6:21:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExerciseTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_ExerciseType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoutineExercises]    Script Date: 3/17/2023 6:21:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoutineExercises](
	[RoutineId] [int] NOT NULL,
	[ExerciseId] [int] NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_RoutineExercises] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Routines]    Script Date: 3/17/2023 6:21:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Routines](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](25) NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_Routines] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 3/17/2023 6:21:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Videos]    Script Date: 3/17/2023 6:21:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Videos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VideoData] [varbinary](max) NOT NULL,
	[WorkoutExerciseId] [int] NOT NULL,
	[Hash] [varbinary](50) NOT NULL,
 CONSTRAINT [PK_Videos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkoutExerciseModifiers]    Script Date: 3/17/2023 6:21:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkoutExerciseModifiers](
	[WorkoutExerciseId] [int] NOT NULL,
	[ExerciseModifierId] [int] NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_WorkoutExerciseModifiers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkoutExercises]    Script Date: 3/17/2023 6:21:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkoutExercises](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[WorkoutId] [int] NOT NULL,
	[ExerciseId] [int] NOT NULL,
	[Sequence] [int] NOT NULL,
	[Notes] [varchar](1000) NULL,
	[FtoWeekNumber] [int] NULL,
	[FtoTrainingMax] [int] NULL,
 CONSTRAINT [PK_WorkoutExercises] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkoutExerciseSets]    Script Date: 3/17/2023 6:21:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkoutExerciseSets](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[WorkoutExerciseId] [int] NOT NULL,
	[SetNumber] [int] NOT NULL,
	[MilesDistance] [float] NULL,
	[MachineIntensity] [int] NULL,
	[MachineWatts] [float] NULL,
	[MachineIncline] [int] NULL,
	[Calories] [int] NULL,
	[MaxBpm] [int] NULL,
	[DurationInSeconds] [int] NULL,
	[Weight] [float] NULL,
	[RepCount] [int] NULL,
	[Volume] [float] NULL,
 CONSTRAINT [PK_WorkoutExerciseSets] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Workouts]    Script Date: 3/17/2023 6:21:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Workouts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NOT NULL,
	[UserId] [int] NOT NULL,
	[Name] [varchar](50) NULL,
 CONSTRAINT [PK_Workouts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BodyMetricRecords]  WITH CHECK ADD  CONSTRAINT [FK_BodyMetricRecords_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[BodyMetricRecords] CHECK CONSTRAINT [FK_BodyMetricRecords_Users]
GO
ALTER TABLE [dbo].[Exercises]  WITH CHECK ADD  CONSTRAINT [FK_Exercises_ExerciseTypes] FOREIGN KEY([ExerciseTypeId])
REFERENCES [dbo].[ExerciseTypes] ([Id])
GO
ALTER TABLE [dbo].[Exercises] CHECK CONSTRAINT [FK_Exercises_ExerciseTypes]
GO
ALTER TABLE [dbo].[RoutineExercises]  WITH CHECK ADD  CONSTRAINT [FK_RoutineExercises_Exercises] FOREIGN KEY([ExerciseId])
REFERENCES [dbo].[Exercises] ([Id])
GO
ALTER TABLE [dbo].[RoutineExercises] CHECK CONSTRAINT [FK_RoutineExercises_Exercises]
GO
ALTER TABLE [dbo].[RoutineExercises]  WITH CHECK ADD  CONSTRAINT [FK_RoutineExercises_Routines] FOREIGN KEY([RoutineId])
REFERENCES [dbo].[Routines] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RoutineExercises] CHECK CONSTRAINT [FK_RoutineExercises_Routines]
GO
ALTER TABLE [dbo].[Routines]  WITH CHECK ADD  CONSTRAINT [FK_Routines_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Routines] CHECK CONSTRAINT [FK_Routines_Users]
GO
ALTER TABLE [dbo].[Videos]  WITH CHECK ADD  CONSTRAINT [FK_Videos_WorkoutExercises] FOREIGN KEY([WorkoutExerciseId])
REFERENCES [dbo].[WorkoutExercises] ([Id])
GO
ALTER TABLE [dbo].[Videos] CHECK CONSTRAINT [FK_Videos_WorkoutExercises]
GO
ALTER TABLE [dbo].[WorkoutExerciseModifiers]  WITH CHECK ADD  CONSTRAINT [FK_WorkoutExerciseModifiers_ExerciseModifiers] FOREIGN KEY([ExerciseModifierId])
REFERENCES [dbo].[ExerciseModifiers] ([Id])
GO
ALTER TABLE [dbo].[WorkoutExerciseModifiers] CHECK CONSTRAINT [FK_WorkoutExerciseModifiers_ExerciseModifiers]
GO
ALTER TABLE [dbo].[WorkoutExerciseModifiers]  WITH CHECK ADD  CONSTRAINT [FK_WorkoutExerciseModifiers_WorkoutExercises] FOREIGN KEY([WorkoutExerciseId])
REFERENCES [dbo].[WorkoutExercises] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[WorkoutExerciseModifiers] CHECK CONSTRAINT [FK_WorkoutExerciseModifiers_WorkoutExercises]
GO
ALTER TABLE [dbo].[WorkoutExercises]  WITH CHECK ADD  CONSTRAINT [FK_WorkoutExercises_Exercises] FOREIGN KEY([ExerciseId])
REFERENCES [dbo].[Exercises] ([Id])
GO
ALTER TABLE [dbo].[WorkoutExercises] CHECK CONSTRAINT [FK_WorkoutExercises_Exercises]
GO
ALTER TABLE [dbo].[WorkoutExercises]  WITH CHECK ADD  CONSTRAINT [FK_WorkoutExercises_WorkoutExercises] FOREIGN KEY([Id])
REFERENCES [dbo].[WorkoutExercises] ([Id])
GO
ALTER TABLE [dbo].[WorkoutExercises] CHECK CONSTRAINT [FK_WorkoutExercises_WorkoutExercises]
GO
ALTER TABLE [dbo].[WorkoutExercises]  WITH CHECK ADD  CONSTRAINT [FK_WorkoutExercises_Workouts] FOREIGN KEY([WorkoutId])
REFERENCES [dbo].[Workouts] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[WorkoutExercises] CHECK CONSTRAINT [FK_WorkoutExercises_Workouts]
GO
ALTER TABLE [dbo].[WorkoutExerciseSets]  WITH CHECK ADD  CONSTRAINT [FK_WorkoutExerciseSets_WorkoutExercises] FOREIGN KEY([WorkoutExerciseId])
REFERENCES [dbo].[WorkoutExercises] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[WorkoutExerciseSets] CHECK CONSTRAINT [FK_WorkoutExerciseSets_WorkoutExercises]
GO
ALTER TABLE [dbo].[Workouts]  WITH CHECK ADD  CONSTRAINT [FK_Workouts_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Workouts] CHECK CONSTRAINT [FK_Workouts_Users]
GO
ALTER TABLE [dbo].[Workouts]  WITH CHECK ADD  CONSTRAINT [FK_Workouts_Workouts] FOREIGN KEY([Id])
REFERENCES [dbo].[Workouts] ([Id])
GO
ALTER TABLE [dbo].[Workouts] CHECK CONSTRAINT [FK_Workouts_Workouts]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetLatestWorkoutExcerciseIds]    Script Date: 3/17/2023 6:21:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create   proc [dbo].[sp_GetLatestWorkoutExcerciseIds] @exerciseIds varchar(50) as
begin
declare @ExerciseIdTable table (ExerciseId int)
insert into @ExerciseIdTable
	select value from string_split(@exerciseIds, ',')

SELECT WorkoutExerciseId
FROM (
        SELECT *, row_number() OVER (PARTITION BY ExerciseId ORDER BY [Date] DESC) as rn
        FROM (
		select we.ExerciseId, w.Date, we.id 'WorkoutExerciseId'
		from Workouts w
		inner join WorkoutExercises we on w.Id = we.WorkoutId
		inner join @ExerciseIdTable et on we.ExerciseId = et.ExerciseId
		) as bobo
        ) as froro
WHERE rn = 1


end
GO

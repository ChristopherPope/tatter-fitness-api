--select * from Workouts order by date desc

--begin Transaction
declare @woId int 
declare @weId int
declare @exId int

select @woId = 13750
select @exId = 2164
select @weId = 17971

insert into WorkoutExercises(WorkoutId, ExerciseId, Sequence, Notes) values(@woId, @exId, 3, null)
select @weId = @@IDENTITY

--reps and weight
insert into WorkoutExerciseSets(WorkoutExerciseId, SetNumber, Weight, RepCount, Volume) values(@weId, 1, 32.5, 10, 325)
insert into WorkoutExerciseSets(WorkoutExerciseId, SetNumber, Weight, RepCount, Volume) values(@weId, 2, 32.5, 10, 325)
insert into WorkoutExerciseSets(WorkoutExerciseId, SetNumber, Weight, RepCount, Volume) values(@weId, 3, 32.5, 10, 325)
insert into WorkoutExerciseSets(WorkoutExerciseId, SetNumber, Weight, RepCount, Volume) values(@weId, 4, 32.5, 10, 325)
insert into WorkoutExerciseSets(WorkoutExerciseId, SetNumber, Weight, RepCount, Volume) values(@weId, 5, 32.5, 10, 325)

----reps only
--insert into WorkoutExerciseSets(WorkoutExerciseId, SetNumber, RepCount) values(@weId, 1, 16)
--insert into WorkoutExerciseSets(WorkoutExerciseId, SetNumber, RepCount) values(@weId, 2, 15)
--insert into WorkoutExerciseSets(WorkoutExerciseId, SetNumber, RepCount) values(@weId, 3, 12)
--insert into WorkoutExerciseSets(WorkoutExerciseId, SetNumber, RepCount) values(@weId, 4, 12)
--insert into WorkoutExerciseSets(WorkoutExerciseId, SetNumber, RepCount) values(@weId, 5, 11)






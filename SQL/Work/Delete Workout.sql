delete from Workouts where
Id in (select Id from Workouts where not Id in (select WorkoutId from WorkoutExercises))





declare @workoutId as int = 16808

delete WorkoutExerciseSets 
	from WorkoutExerciseSets s
	join WorkoutExercises we on s.WorkoutExerciseId = we.Id
	where we.WorkoutId = @workoutId

delete WorkoutExerciseModifiers 
	from WorkoutExerciseModifiers m
	join WorkoutExercises we on m.WorkoutExerciseId = we.Id
	where we.WorkoutId = @workoutId

delete Videos 
	from Videos v
	join WorkoutExercises we on v.WorkoutExerciseId = we.Id
	where we.WorkoutId = @workoutId

delete from WorkoutExercises where WorkoutId = @workoutId
delete from Workouts where id = @workoutId


select * from Workouts order by id desc
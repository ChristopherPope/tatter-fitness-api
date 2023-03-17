declare @workoutId as int
declare @weId as int
declare @exerciseId as int
declare @exTypeId as int
declare @exTypeName as varchar(25)
declare @mods table(WorkoutExerciseId int, Mods varchar(1000))


select @workoutId = max(id) from Workouts
--select @workoutId = 17812

insert into @mods
select we.id, STRING_AGG(em.Name, ', ') from WorkoutExerciseModifiers m
inner join ExerciseModifiers em on m.ExerciseModifierId = em.Id
inner join WorkoutExercises we on m.WorkoutExerciseId = we.Id
inner join Exercises e on we.ExerciseId = e.Id
where we.WorkoutId = @workoutId
group by we.Id

select w.Date, w.Id 'WorkoutId', w.Name, we.Id 'WorkoutExerciseId', we.ExerciseId, we.Sequence, e.Name as ExerciseName,	
et.Name as ExerciseType, we.Notes, m.Mods
from Workouts w
left outer join WorkoutExercises we on w.Id = we.WorkoutId
left outer join Exercises e on we.ExerciseId = e.Id
left outer join ExerciseTypes et on et.Id = e.ExerciseTypeId
left outer join @mods m on we.Id = m.WorkoutExerciseId
where w.Id = @workoutId
order by we.Sequence, m.Mods

declare cur1 cursor for 
select we.Id "WorkoutExerciseId", e.Id "ExerciseId", e.ExerciseTypeId, et.Name
from WorkoutExercises we
join Exercises e on we.ExerciseId = e.Id
join ExerciseTypes et on e.ExerciseTypeId = et.Id
where we.WorkoutId = @workoutId
order by we.Sequence

open cur1
fetch next from cur1 into @weId, @exerciseId, @exTypeId, @exTypeName

while (@@FETCH_STATUS = 0)
begin
	if (@exTypeId = 1)	-- Cardio
		select we.id "WorkoutExerciseId", e.Id "ExcerciseId", s.id "SetId", e.Name "Exercise", @exTypeName as ExerciseType, s.SetNumber, s.id "SetId", s.MilesDistance, s.DurationInSeconds, s.MachineIncline, s.MachineIntensity, s.MachineWatts, s.Calories, s.MaxBPM
		from WorkoutExercises we
		join Exercises e on we.ExerciseId = e.Id
		join WorkoutExerciseSets s on we.id = s.WorkoutExerciseId
		where we.id = @weId and we.ExerciseId = @exerciseId
		order by s.SetNumber
	else if (@exTypeId = 2) -- DurationAndWeight
		select we.id "WorkoutExerciseId", e.Id "ExcerciseId", s.id "SetId", e.Name "Exercise", @exTypeName as ExerciseType, s.SetNumber, s.id "SetId", s.DurationInSeconds, s.Weight
		from WorkoutExercises we
		join Exercises e on we.ExerciseId = e.Id
		join WorkoutExerciseSets s on we.id = s.WorkoutExerciseId
		where we.id = @weId and we.ExerciseId = @exerciseId
		order by s.SetNumber
	else if (@exTypeId = 3) -- RepsAndWeight
		select we.id "WorkoutExerciseId", e.Id "ExcerciseId", s.id "SetId", e.Name "Exercise", @exTypeName as ExerciseType, s.SetNumber, s.RepCount, s.Weight, s.Volume
		from WorkoutExercises we
		join Exercises e on we.ExerciseId = e.Id
		join WorkoutExerciseSets s on we.id = s.WorkoutExerciseId
		where we.id = @weId and we.ExerciseId = @exerciseId
		order by s.SetNumber
	else if (@exTypeId = 4) -- RepsOnly
		select we.id "WorkoutExerciseId", e.Id "ExcerciseId", s.id "SetId", e.Name "Exercise", @exTypeName as ExerciseType, s.SetNumber, s.RepCount
		from WorkoutExercises we
		join Exercises e on we.ExerciseId = e.Id
		join WorkoutExerciseSets s on we.id = s.WorkoutExerciseId
		where we.id = @weId and we.ExerciseId = @exerciseId
		order by s.SetNumber

	fetch next from cur1 into @weId, @exerciseId, @exTypeId, @exTypeName
end

close cur1
deallocate cur1






update WorkoutExerciseSets set RepCount = 10 where id = 9236

select * from WorkoutExerciseSets where id = 9326





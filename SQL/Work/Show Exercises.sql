select e.Id, e.Name as ExerciseName, et.Name as ExerciseType, et.Id as SetTypeId, e.IsActive, count(we.id) as ExerciseCount
from Exercises e
inner join ExerciseTypes et on e.ExerciseTypeId = et.id
left outer join WorkoutExercises we on we.ExerciseId = e.Id
group by e.Id, e.Name, et.Name, et.Id, e.IsActive
order by 
e.Name 
--ExerciseCount 



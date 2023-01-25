select re.Id as RoutineExerciseId, r.Id as RoutineId, r.Name as RoutineName, e.Id as ExerciseId, e.Name as Exercise
from Routines r
left outer join RoutineExercises re on r.id = re.RoutineId
left outer join Exercises e on re.ExerciseId = e.Id
order by r.name, e.Name



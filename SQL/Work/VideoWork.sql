select * from VideosStage
select * from Videos
select count(0) from Videos

update VideosStage set CreatedDate = '10/7/20'

-- Match VideoStage to Workout
select w.Id 'WorkoutId', w.Date, we.Id 'WorkoutExerciseId', e.Name
from Workouts w
inner join WorkoutExercises we on w.Id = we.WorkoutId
inner join Exercises e on we.ExerciseId = e.Id
inner join VideosStage vs on w.Date 
	between DateTimeFromParts(year(vs.CreatedDate), month(vs.CreatedDate), day(vs.CreatedDate), 0, 0, 0, 0)
	and DateTimeFromParts(year(vs.CreatedDate), month(vs.CreatedDate), day(vs.CreatedDate), 23, 59, 59, 999)


-- Move video from stage
insert into Videos(Id, CreatedDate, VideoData, WorkoutExerciseId)
select Id, CreatedDate, VideoData, 9762 from VideosStage 

delete from VideosStage 
select * from VideosStage


-- Duplicate match VideosStage
select g.Count, v.Id from
( select VideoData, count(0) as Count from VideosStage group by VideoData ) g
inner join Videos v on g.VideoData = v.VideoData
where g.Count > 1




-- Duplicate match of Videos to VideosStage
SELECT v.id 'VideoId', s.id 'StageVideoId'
from Videos v
inner join VideosStage s on v.VideoData = s.VideoData

update VideosStage set CreatedDate = '08/25/2021'

delete from videos where id = 26

delete from VideosStage where Id = 39

select * from Workouts order by Date





# TatterFitness REST API
This is the REST API for the [TatterFitness mobile app](https://github.com/ChristopherPope/tatter-fitness-mobile) I wrote. It is written in .Net 6.0 and soon to be upgraded to 7.0 as soon as I find the time.

- [TatterFitness REST API](#tatterfitness-rest-api)
- [Database Design](#database-design)
  - [ExerciseTypes Table](#exercisetypes-table)
  - [ExerciseModifiers Table](#exercisemodifiers-table)
  - [Storing a Workout](#storing-a-workout)
    - [Workout table](#workout-table)
    - [WorkoutExercises and WorkoutExerciseSets tables](#workoutexercises-and-workoutexercisesets-tables)
  - [Videos](#videos)
  - [Routines](#routines)
- [Database Diagram](#database-diagram)
- [Swagger UI](#swagger-ui)




# Database Design

## ExerciseTypes Table
There are 4 types of exercises in TF, these are stored in the ***ExerciseTypes*** table.
- Reps & Weight (e.g. Bench Press or Squat)
- Cardio (e.g. Elliptical)
- Duration & Weight (e.g. Timed Farmer's Carry)
- Reps Only (e.g. Goodmornings or Leg Raises)

## ExerciseModifiers Table
When a workout is performed, modifiers may be added to the exercise. These are stored in the ***ExerciseModifiers*** table, some modifiers are:
- Narrow Grip
- Medium Grip
- PBand
- RBand

## Storing a Workout
A workout is stored in 4 tables:
- ***Workout***
- ***WorkoutExerciseSets***
- ***WorkoutExercises***
- ***WorkoutExerciseModifiers***

### Workout table
When a workout is performed, one row is inserted here.

### WorkoutExercises and WorkoutExerciseSets tables
A lifter will normally perform the workout in sets. When a workout exercise is performed (e.g. Bench Press) a row is stored in the ***WorkoutExercises*** table with the sets are stored in the ***WorkoutExerciseSets*** table.

## Videos
Workout videos are stored in the ***Videos*** table with a foreign key to the ***WorkoutExercises*** table.

## Routines
A user may store multiple routines in the ***Routines*** table with routine exercises stored in the ***RoutineExercises*** table.







# Database Diagram
<img src="/DbDiagram.jpg"></img>

# Swagger UI
<img src="/SwaggerUI.jpeg"></img>


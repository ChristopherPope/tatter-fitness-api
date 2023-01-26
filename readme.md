# TatterFitness REST API
This is the REST API for the [TatterFitness mobile app](https://github.com/ChristopherPope/tatter-fitness-mobile) I wrote. It is written in .Net 6.0 and soon to be upgraded to 7.0 as soon as I find the time.

- [TatterFitness REST API](#tatterfitness-rest-api)
- [Database Design](#database-design)
  - [Exercise Types](#exercise-types)
  - [Exercise Modifiers](#exercise-modifiers)
  - [Storing a Workout](#storing-a-workout)
    - [***Workout*** table](#workout-table)
- [Swagger UI](#swagger-ui)




# Database Design

## Exercise Types
There are 4 types of exercises in TF:
- Reps & Weight (e.g. Bench Press or Squat)
- Cardio (e.g. Elliptical)
- Duration & Weight (e.g. Timed Farmer's Carry)
- Reps Only (e.g. Goodmornings or Leg Raises)

## Exercise Modifiers
When a workout is performed, certain modifiers may have been made to an exercise. Some modifiers are:
- Narrow Grip
- Medium Grip
- PBand
- RBand

Modifiers are stored in the ***ExerciseModifiers*** table.

## Storing a Workout
A workout is stored in 4 tables:
- ***Workout***
- ***WorkoutExerciseSets***
- ***WorkoutExercises***
- ***WorkoutExerciseModifiers***

### ***Workout*** table

| Name  | Tye   | Allow Nulls  |
| ------------ | ------------ | ------------ |
| Id  | int   | Not NULL  |
| Date  | DateTime   | Not NULL  |
| UserId  | int  | Not NULL   |
| Name  | varchar(50)  | NULL  |











# Swagger UI
<img src="/SwaggerUI.jpeg"></img>


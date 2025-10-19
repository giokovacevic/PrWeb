export default interface IQiozResultResponse{
    readonly id: number;
    readonly quizName: string;
    readonly quizDifficultyValue: string;
    readonly userUsername: string;
    readonly time: number;
    readonly date: string;
    readonly answersCount: number;
    readonly correctAnswers: number;
    readonly correctAnswersPercentage: number;
    readonly points: number;
    readonly maxPoints: number;
    readonly pointsPercentage: number;
}
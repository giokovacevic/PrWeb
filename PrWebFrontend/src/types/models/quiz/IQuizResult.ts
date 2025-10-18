import type IAnswer from "./IAnswer";

export default interface IQuizResult {
    userId?: number;
    quizId: number,
    timeNeededSeconds: number;
    answers: IAnswer[];
}
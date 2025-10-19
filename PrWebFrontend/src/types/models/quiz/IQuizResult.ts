import type IAnswer from "./IAnswer";

export default interface IQuizResult {
    userId?: number;
    userUsername?: string;
    quizId: number,
    timeNeededSeconds: number;
    answers: IAnswer[];
}
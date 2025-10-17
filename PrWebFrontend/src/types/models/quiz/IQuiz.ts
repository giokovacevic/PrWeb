import type IQuestion from "./IQuestion";
import type IQuizDifficulty from "./IQuizDifficulty";

export default interface IQuiz{
    readonly id: number;
    readonly name: string;
    readonly timeSeconds: number;
    readonly difficulty: IQuizDifficulty;
    readonly description: string;
    readonly questions: IQuestion[];
}
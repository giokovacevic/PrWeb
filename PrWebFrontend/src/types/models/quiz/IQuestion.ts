import type IOption from "./IOption";

export type QuestionType = "FILL_IN" | "TRUE_FALSE" | "SINGLE_CHOICE" | "MULTI_CHOICE";

export default interface IQuestion{
    readonly id: number;
    readonly text: string;
    readonly theme: string;
    readonly type: QuestionType;
    readonly answer: string | null;
    readonly isCorrect: boolean | null;
    readonly options: IOption[] | null;
}
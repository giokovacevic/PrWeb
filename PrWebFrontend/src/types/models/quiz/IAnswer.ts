export default interface IAnswer{
    questionId: number;
    answer?: string;
    correct?: boolean | null;
    selectionId?: number | null;
    optionIds: number[] | null;
}
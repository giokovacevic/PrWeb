export const getDifficultyColor = (value:string) => {
        switch(value) {
            case "Easy":
                return "#00b457ff";
            case "Medium":
                return "#d18f00ff";
            case "Hard":
                return "#d80e00ff";
            default:
                return "";
        }
    }
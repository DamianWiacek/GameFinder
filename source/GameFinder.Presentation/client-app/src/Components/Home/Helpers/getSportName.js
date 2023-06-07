export const getSportName = (gameType) => {
    switch (gameType) {
      case 1:
        return "Piłka Nożna";
      case 2:
        return "Koszykówka";
      case 3:
        return "Siatkówka";
      case 4:
        return "Tenis";
      default:
        return "Select Sport";
    }
};
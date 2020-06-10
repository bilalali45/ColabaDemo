export const MaskPhone = (number: number) => {
    return String(number).split('').map((n, i) => {
        if (i === 0) {
            return `(${n}`
        }
        if (i === 2) {
            return `${n}) `
        }

        if (i === 5) {
            return `${n} - `
        }

        return n;
    }).join('');
};

export const UnMaskPhone = (formattedNumber: any) => {
	return formattedNumber.split('').filter(n => {
        if (!isNaN(n) && n !== ' ') {
            return n
        }
    }).join('');
}




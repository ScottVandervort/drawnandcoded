if (!Array.prototype.move) {

	// Description : Moves an element in an array.
	// Params:          old_index - The old index of element.
	//                  new_index - The new index of element.
	// http://stackoverflow.com/questions/5306680/move-an-array-element-from-one-array-position-to-another
	Array.prototype.move = function (old_index, new_index) {
		if (new_index >= this.length) {
			var k = new_index - this.length;
			while ((k--) + 1) {
				this.push(undefined);
			}
		}
		this.splice(new_index, 0, this.splice(old_index, 1)[0]);
		return this; // for testing purposes
	};
}

if (!Array.prototype.indexOf) {

	// Description : Finds the index of an object in the specified array
	// Params:          obj - The object.
	//                  start - The index to start looking for the object in the aray.
	// http: //stackoverflow.com/questions/1744310/how-to-fix-array-indexof-in-javascript-for-ie-browsers
	Array.prototype.indexOf = function (obj, start) {
		for (var i = (start || 0), j = this.length; i < j; i++) {
			if (this[i] === obj) { return i; }
		}
		return -1;
	}
}


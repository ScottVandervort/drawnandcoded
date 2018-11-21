QUnit.module( "Extensions",
			{ 	setup     : function() {},
				teardown  : function() {} 
			});
                
QUnit.test( "move() extends Array", 2, function() {

	notEqual(Array.prototype.move, undefined);
	notEqual(Array.prototype.move, null);        
});
                
QUnit.test( "Array.move to first", 5 , function() {
	
	var array = [ "Homer", "Bart", "Maggie", "Lisa", "Marge" ];
  
	array.move(4,0);
  
	equal( array[0], "Marge" );
	notEqual( array[1], "Marge" );
	notEqual( array[2], "Marge" );
	notEqual( array[3], "Marge" );
	notEqual( array[4], "Marge" );  
});

QUnit.test( "Array.move to last", 5 , function() {
	
	var array = [ "Homer", "Bart", "Maggie", "Lisa", "Marge" ];

	array.move(0,4);

	equal( array[4], "Homer" );
	notEqual( array[3], "Homer" );
	notEqual( array[2], "Homer" );
	notEqual( array[1], "Homer" );
	notEqual( array[0], "Homer" );
});

QUnit.test( "Array.move to arbitrary", 5 , function() {
	
	var array = [ "Homer", "Bart", "Maggie", "Lisa", "Marge" ];

	array.move(2,3);

	equal( array[3], "Maggie" );
	notEqual( array[0], "Maggie" );
	notEqual( array[1], "Maggie" );
	notEqual( array[2], "Maggie" );
	notEqual( array[4], "Maggie" );
});

QUnit.test( "indexOf() extends Array", 2, function() {

	notEqual(Array.prototype.indexOf, undefined);
	notEqual(Array.prototype.indexOf, null);        
});

QUnit.test( "Array.indexOf exists", 1 , function() {
	
	var array = [ "Homer", "Bart", "Maggie", "Lisa", "Marge" ];

	equal( array.indexOf("Homer"), 0 );  
});

QUnit.test( "Array.indexOf does not exist", 1 , function() {
	
	var array = [ "Homer", "Bart", "Maggie", "Lisa", "Marge" ];

	equal( array.indexOf("Mickey"), -1 );
});


using IntelligentHack.IntelligentCache;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace IntelligentCache.Tests
{
    public class JsonStringSerializerTests
    {
        [Fact]
        public void Serialize_does_not_include_the_type_name_when_the_base_type_is_passed()
        {
            // Arrange
            var sut = new JsonStringSerializer();

            // Act
            var json = sut.Serialize(new Base { A = 3 }).ToString();

            // Assert
            Assert.Equal("{\"A\":3}", json);
        }

        [Fact]
        public void Serialize_includes_the_type_name_when_a_derived_class_is_passed()
        {
            // Arrange
            var sut = new JsonStringSerializer();

            // Act
            var json = sut.Serialize<Base>(new Derived { A = 3, B = 5 }).ToString();

            // Assert
            Assert.Equal($"{{\"$type\":\"{typeof(Derived).FullName}, {typeof(Derived).Assembly.GetName().Name}\",\"B\":5,\"A\":3}}", json);
        }

        [Fact]
        public void Deserialize_used_the_specified_type_by_default()
        {
            // Arrange
            var sut = new JsonStringSerializer();

            // Act
            var value = sut.Deserialize<Base>("{\"A\":3}");

            // Assert
            Assert.IsType<Base>(value);
            Assert.Equal(3, value.A);
        }

        [Fact]
        public void Deserialize_honors_the_type_from_the_payload()
        {
            // Arrange
            var sut = new JsonStringSerializer();

            // Act
            var value = sut.Deserialize<Base>($"{{\"$type\":\"{typeof(Derived).FullName}, {typeof(Derived).Assembly.GetName().Name}\",\"B\":5,\"A\":3}}");

            // Assert
            var derived = Assert.IsType<Derived>(value);
            Assert.Equal(3, derived.A);
            Assert.Equal(5, derived.B);
        }

        class Base
        {
            public int A { get; set; }
        }

        class Derived : Base
        {
            public int B { get; set; }
        }
    }
}
